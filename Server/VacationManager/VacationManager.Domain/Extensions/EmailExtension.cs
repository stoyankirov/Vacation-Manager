namespace VacationManager.Domain.Extensions
{
    using System.ComponentModel.DataAnnotations;

    public static class EmailExtension
    {
        public static bool IsValidEmail(this string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
                return true;

            return false;
        }
    }
}
