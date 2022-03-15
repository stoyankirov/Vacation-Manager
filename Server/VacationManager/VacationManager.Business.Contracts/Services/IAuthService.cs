namespace VacationManager.Business.Contracts.Services
{
    using VacationManager.Domain.Requests;

    public interface IAuthService
    {
        void Register(RegisterRequest request);
    }
}
