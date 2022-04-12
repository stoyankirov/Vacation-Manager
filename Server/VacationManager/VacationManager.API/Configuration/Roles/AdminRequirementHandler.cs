namespace VacationManager.API.Configuration.Roles
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using VacationManager.Domain.Enums;

    public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(claim => claim.Type == "IsInRole");

            if (!hasClaim)
            {
                return Task.CompletedTask;
            }

            var claimValue = context.User.FindFirst(claim => claim.Type == "IsInRole").Value;
            var role = (int)Role.Admin;

            var isAdmin = int.Parse(claimValue).Equals(role);

            if (claimValue != null && isAdmin)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
