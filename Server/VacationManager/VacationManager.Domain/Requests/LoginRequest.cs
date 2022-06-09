namespace VacationManager.Domain.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRequest : Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
