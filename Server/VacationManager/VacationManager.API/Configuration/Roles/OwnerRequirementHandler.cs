namespace VacationManager.API.Configuration.Roles
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using VacationManager.Domain.Enums;

    public class OwnerRequirementHandler : AuthorizationHandler<OwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(claim => claim.Type == "Role");

            if (!hasClaim)
            {
                return Task.CompletedTask;
            }

            var claimValue = context.User.FindFirst(claim => claim.Type == "Role").Value;
            var isOwner = context.User.FindFirst(claim => claim.Type == "Role").Value.Equals(Role.Owner);

            if (claimValue != null && isOwner)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
