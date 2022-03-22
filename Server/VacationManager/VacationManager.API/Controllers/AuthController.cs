namespace VacationManager.API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Domain.Enums;
    using VacationManager.Domain.Models;
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
            bool userExists = this._authService.UserExists(request.Email);

            if (userExists)
            {
                return Conflict(new Message(StatusCodes.Status409Conflict, MessageCode.UserExists));
            }

            this._authService.Register(request);

            return Ok();
        }
    }
}
