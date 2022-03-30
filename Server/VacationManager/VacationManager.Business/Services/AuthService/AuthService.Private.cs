namespace VacationManager.Business.Services.AuthService
{
    using System;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Domain;
    using VacationManager.Domain.Entities;
    using VacationManager.Domain.Requests;

    public partial class AuthService : Service, IAuthService
    {
        protected override void ValidateRequest(Request request)
        {
            base.ValidateRequest(request);

            if (request is RegisterRequest)
            {
                var registerRequest = request as RegisterRequest;

                if (registerRequest.UserRole != Domain.Enums.Role.Admin &&
                    registerRequest.UserRole != Domain.Enums.Role.User &&
                    registerRequest.UserRole != Domain.Enums.Role.Owner)
                {
                    throw new ArgumentException($"Invalid {nameof(Domain.Enums.Role)}");
                }
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
    }
}
