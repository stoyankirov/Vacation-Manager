namespace VacationManager.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Domain.Requests;

    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterRequest request)
        {
            this._authService.Register(request);

            return Ok();
        }
    }
}
