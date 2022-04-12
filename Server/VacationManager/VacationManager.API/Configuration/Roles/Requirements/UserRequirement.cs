namespace VacationManager.API.Configuration.Roles
{
    using Microsoft.AspNetCore.Authorization;

    public class UserRequirement : IAuthorizationRequirement
    {
        public UserRequirement(bool isUser)
        {
            this.IsUser = isUser;
        }

        public bool IsUser { get; set; }
    }
}
