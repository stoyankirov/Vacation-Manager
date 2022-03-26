namespace VacationManager.Business.Services.AuthService
{
    using System;
    using System.Transactions;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Core.Utility;
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;
    using VacationManager.Domain.Requests;

    public partial class AuthService : Service, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfirmRegistrationCodeRepository _confirmRegistrationCodeRepository;

        public AuthService(IUserRepository userRepository,
            IConfirmRegistrationCodeRepository confirmRegistrationCodeRepository)
        {
            this._userRepository = userRepository;
            this._confirmRegistrationCodeRepository = confirmRegistrationCodeRepository;
        }

        public bool UserExists(string email)
            => this._userRepository.GetUserByEmail(email) != null
                ? true
                : false;

        public void Register(RegisterRequest request)
        {
            this.ValidateRequest(request);

            var passwordSalt = string.Empty;
            var passwordHash = PasswordHasher
                .Hash(request.Password, out passwordSalt);

            using (TransactionScope scope = new TransactionScope())
            {
                var codeId = this.GenerateConfirmRegistrationCode();

                var userEntity = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    ConfirmRegistrationCodeId = codeId
                };

                this._userRepository.AddAsync(userEntity);

                scope.Complete();
            }
        }
    }
}
