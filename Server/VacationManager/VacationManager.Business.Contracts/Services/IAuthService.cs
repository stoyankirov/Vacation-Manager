namespace VacationManager.Business.Contracts.Services
{
    using VacationManager.Domain.Requests;

    public interface IAuthService
    {
        bool UserExists(string email);
        void Register(RegisterRequest request);
    }
}
