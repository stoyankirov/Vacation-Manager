namespace VacationManager.Business.Services.AuthService
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Core.Utility;
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;
    using VacationManager.Domain.Requests;
    using VacationManager.Domain.Responses;

    public partial class AuthService : Service, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IConfirmRegistrationCodeRepository _confirmRegistrationCodeRepository;
        private readonly INotificationService _notificationService;

        public AuthService(IUserRepository userRepository,
            IJwtUtils jwtUtils,
            IConfirmRegistrationCodeRepository confirmRegistrationCodeRepository,
            INotificationService notificationService)
        {
            this._userRepository = userRepository;
            this._jwtUtils = jwtUtils;
            this._confirmRegistrationCodeRepository = confirmRegistrationCodeRepository;
            this._notificationService = notificationService;
        }

        public bool UserExists(string email)
            => this._userRepository.GetUserByEmail(email) != null
                ? true
                : false;

        public Guid Register(RegisterRequest request)
        {
            this.ValidateRequest(request);

            var passwordSalt = PasswordHasher.GeneratePasswordSalt();

            var passwordHash = PasswordHasher
                .Hash(request.Password, passwordSalt);

            var userId = Guid.Empty;

            using (TransactionScope scope = new TransactionScope())
            {
                var confirmationCode = this.GenerateConfirmRegistrationCode();

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = (int)request.UserRole,
                    ConfirmRegistrationCodeId = confirmationCode.Id
                };

                this._userRepository.AddAsync(user);

                this._notificationService
                    .SendRegisterConfirmationEmail(user.Email, confirmationCode.Code);

                userId = user.Id;

                scope.Complete();
            }

            return userId;
        }

        public async Task<bool> ConfirmRegistration(ConfirmRegistrationRequest request)
        {
            this.ValidateRequest(request);

            bool successfullyConfirmed = false;

            var user = await this._userRepository
                .GetByIdAsync(request.UserId);

            if (user.ConfirmRegistrationCode.Code == request.ConfirmationCode)
            {
                successfullyConfirmed = true;
                user.IsConfirmed = true;
                await this._userRepository
                    .UpdateAsync(user);
            }

            return successfullyConfirmed;
        }

        public LoginResponse Login(LoginRequest request)
        {
            this.ValidateRequest(request);

            var user = this._userRepository
                .GetUserByEmail(request.Email);

            this.ValidateUser(user);
            this.ValidatePassword(user, request.Password);

            var token = this._jwtUtils.GenerateJwtToken(user);

            var response = new LoginResponse(user.Email, token);

            return response;
        }
    }
}
