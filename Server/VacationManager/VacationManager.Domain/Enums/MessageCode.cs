namespace VacationManager.Domain.Enums
{
    public enum MessageCode
    {
        Unknown = 0,
        UserExists = 1,
        IncorrectConfirmationCode = 2,
        NotExistingUser = 3,
        AccountNotConfirmed = 4,
        LoginFailed = 5,

        InvalidRole = 10
    }
}
