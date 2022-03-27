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
        private readonly INotificationService _notificationService;

        public AuthService(IUserRepository userRepository,
            IConfirmRegistrationCodeRepository confirmRegistrationCodeRepository,
            INotificationService notificationService)
        {
            this._userRepository = userRepository;
            this._confirmRegistrationCodeRepository = confirmRegistrationCodeRepository;
            this._notificationService = notificationService;
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
                var confirmationCode = this.GenerateConfirmRegistrationCode();

                var userEntity = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = (int)request.UserRole,
                    ConfirmRegistrationCodeId = confirmationCode.Id
                };

                this._userRepository.AddAsync(userEntity);

                this._notificationService.SendRegisterConfirmationEmail(userEntity.Email, confirmationCode.Code);

                scope.Complete();
            }
        }
    }
}
