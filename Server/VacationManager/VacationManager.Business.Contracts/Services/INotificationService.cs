namespace VacationManager.Business.Contracts.Services
{
    public interface INotificationService
    {
        void SendRegisterConfirmationEmail(string email, string code);
    }
}
