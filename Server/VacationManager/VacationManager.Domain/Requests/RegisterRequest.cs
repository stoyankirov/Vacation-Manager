namespace VacationManager.Domain.Requests
{
    using System.ComponentModel.DataAnnotations;
    using VacationManager.Domain.Enums;

    public class RegisterRequest : Request
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [MinLength(4), MaxLength(12)]
        public string Password { get; set; }

        [Required]
        public Role UserRole { get; set; }
    }
}
