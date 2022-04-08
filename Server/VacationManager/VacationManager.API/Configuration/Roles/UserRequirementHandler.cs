namespace VacationManager.API.Configuration.Roles
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using VacationManager.Domain.Enums;

    public class UserRequirementHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(claim => claim.Type == "Role");

            if (!hasClaim)
            {
                return Task.CompletedTask;
            }

            var claimValue = context.User.FindFirst(claim => claim.Type == "Role").Value;
            var isUser = context.User.FindFirst(claim => claim.Type == "Role").Value.Equals(Role.User);

            if (claimValue != null && isUser)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
