namespace VacationManager.Business.Services.AuthService
{
    using FluentResult;
    using System;
    using System.Threading.Tasks;
    using System.Transactions;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Business.Factories;
    using VacationManager.Core.Utility;
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;
    using VacationManager.Domain.Extensions;
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

        public async Task<bool> UserExists(string email)
            => await this._userRepository.GetUserByEmail(email) != null
                ? true
                : false;

        public Guid Register(RegisterRequest request)
        {
            this.ValidateRequest(request);

            var passwordSalt = PasswordHasher.GeneratePasswordSalt();

            var passwordHash = PasswordHasher
                .Hash(request.Password, passwordSalt);

            var userId = Guid.Empty;

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

                //this._notificationService
                //    .SendRegisterConfirmationEmail(user.Email, confirmationCode.Code);

                userId = user.Id;

            return userId;
        }

        public Task<Result<bool>> ConfirmRegistration(ConfirmRegistrationRequest request)
        {
            return Result
                .Create(request)
                .MapAsync(request => this._userRepository.GetByIdAsync(request.UserId))
                .ValidateAsync(user => user != null, ResultComplete.NotFound, "Confirm registration failed.")
                .ValidateAsync(
                    user => !user.ConfirmRegistrationCode.Equals(request.ConfirmationCode),
                    ResultComplete.OperationFailed,
                    "Confirm registration failed.")
                .MapAsync(user =>
                {
                    user.IsConfirmed = true;
                    return user;
                 
                })
                .MapAsync(user => this._userRepository.UpdateAsync(user));
            // return user model
        }

        public Task<Result<LoginResponse>> Login(LoginRequest request)
        {
            return Result
                .Create(request)
                .MapAsync(request => this._userRepository.GetUserByEmail(request.Email))
                .ValidateAsync(user => user != null, ResultComplete.NotFound, "Login failed.")
                .ValidateAsync(user => 
                    user.IsConfirmed == true, 
                    ResultComplete.OperationFailed, 
                    "Registration is not confirmed.")
                .ValidateAsync(
                    user => this.PasswordMatch(user, request.Password) == true,
                    ResultComplete.OperationFailed,
                    "Login failed.")
                .MapAsync(user => this._jwtUtils.GenerateJwtToken(user))
                .MapAsync(AuthFactory.ToLoginResponse);
        }
    }
}
