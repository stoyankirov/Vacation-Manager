namespace VacationManager.Business.Services.AuthService
{
    using System;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Core.Utility;
    using VacationManager.Domain;
    using VacationManager.Domain.Entities;
    using VacationManager.Domain.Enums;
    using VacationManager.Domain.Requests;

    public partial class AuthService : Service, IAuthService
    {
        protected override void ValidateRequest(Request request)
        {
            base.ValidateRequest(request);

            switch (request)
            {
                case RegisterRequest:

                    var registerRequest = request as RegisterRequest;

                    if (registerRequest.UserRole != Domain.Enums.Role.Admin &&
                        registerRequest.UserRole != Domain.Enums.Role.User &&
                        registerRequest.UserRole != Domain.Enums.Role.Owner)
                    {
                        throw new ArgumentException($"Invalid {nameof(Domain.Enums.Role)}");
                    }

                    break;

                case ConfirmRegistrationRequest:

                    var confirmRegistrationRequest = request as ConfirmRegistrationRequest;

                    if (confirmRegistrationRequest.ConfirmationCode == null)
                        throw new ArgumentNullException($"Invalid {nameof(confirmRegistrationRequest.ConfirmationCode)}");

                    if (confirmRegistrationRequest.ConfirmationCode.Length != 6)
                        throw new ArgumentException($"Invalid {nameof(confirmRegistrationRequest.ConfirmationCode)}");

                    if (confirmRegistrationRequest.UserId == Guid.Empty)
                        throw new ArgumentNullException($"Invalid {nameof(ConfirmRegistrationRequest)}");

                    break;
            }
        }

        private ConfirmRegistrationCode GenerateConfirmRegistrationCode()
        {
            ConfirmRegistrationCode confirmationCode = new ConfirmRegistrationCode();
            confirmationCode.Id = Guid.NewGuid();
            confirmationCode.ExpirationDate = DateTime.Now.AddMonths(1);

            Random random = new Random();
            confirmationCode.Code = random.Next(0, 1000000).ToString("D6");

            this._confirmRegistrationCodeRepository.AddAsync(confirmationCode);

            return confirmationCode;
        }

        private void ValidatePassword(User user, string requestedPassword)
        {
            var hash = PasswordHasher.Hash(requestedPassword, user.PasswordSalt);

            if (user.Password != hash)
                throw new ArgumentException("Login failed");
        }
    }
}
