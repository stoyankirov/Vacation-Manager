namespace VacationManager.Business.Contracts.Services
{
    using FluentResult;
    using System;
    using System.Threading.Tasks;
    using VacationManager.Domain.Requests;
    using VacationManager.Domain.Responses;

    public interface IAuthService
    {
        Task<bool> UserExists(string email);

        Guid Register(RegisterRequest request);

        Task<Result<bool>> ConfirmRegistration(ConfirmRegistrationRequest request);

        Task<Result<LoginResponse>> Login(LoginRequest request);
    }
}
