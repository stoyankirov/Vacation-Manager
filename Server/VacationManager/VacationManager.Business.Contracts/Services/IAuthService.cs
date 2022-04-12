namespace VacationManager.Business.Contracts.Services
{
    using System;
    using System.Threading.Tasks;
    using VacationManager.Domain.Requests;
    using VacationManager.Domain.Responses;

    public interface IAuthService
    {
        bool UserExists(string email);

        Guid Register(RegisterRequest request);

        Task<bool> ConfirmRegistration(ConfirmRegistrationRequest request);

        LoginResponse Login(LoginRequest request);
    }
}
