namespace VacationManager.API.Configuration.Roles
{
    using Microsoft.AspNetCore.Authorization;

    public class OwnerRequirement : IAuthorizationRequirement
    {
        public OwnerRequirement(bool isOwner)
        {
            this.IsOwner = isOwner;
        }

        public bool IsOwner { get; set; }
    }
}
