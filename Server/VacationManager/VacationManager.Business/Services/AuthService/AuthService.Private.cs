namespace VacationManager.Business.Services.AuthService
{
    using System;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Domain.Entities;

    public partial class AuthService : Service, IAuthService
    {
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
