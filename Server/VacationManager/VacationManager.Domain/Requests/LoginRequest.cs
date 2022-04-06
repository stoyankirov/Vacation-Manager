namespace VacationManager.Domain.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRequest : Request
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(4), MaxLength(12)]
        public string Password { get; set; }
    }
}
