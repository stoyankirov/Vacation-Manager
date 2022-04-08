namespace VacationManager.API.Configuration.Roles
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using VacationManager.Domain.Enums;

    public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(claim => claim.Type == "Role");

            if (!hasClaim)
            {
                return Task.CompletedTask;
            }

            var claimValue = context.User.FindFirst(claim => claim.Type == "Role").Value;
            var isAdmin = context.User.FindFirst(claim => claim.Type == "Role").Value.Equals(Role.Admin);

            if (claimValue != null && isAdmin)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
