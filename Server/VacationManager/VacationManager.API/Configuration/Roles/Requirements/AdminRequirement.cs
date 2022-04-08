namespace VacationManager.API.Configuration.Roles
{
    using Microsoft.AspNetCore.Authorization;

    public class AdminRequirement : IAuthorizationRequirement
    {
        public AdminRequirement(bool isAdmin)
        {
            this.IsAdmin = isAdmin;
        }

        public bool IsAdmin { get; set; }
    }
}
