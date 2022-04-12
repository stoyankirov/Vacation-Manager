namespace VacationManager.API.Configuration
{
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Threading.Tasks;
    using VacationManager.Core.Utility;

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["UserId"] = userId.Value;
            }

            await _next(context);
        }
    }
}
