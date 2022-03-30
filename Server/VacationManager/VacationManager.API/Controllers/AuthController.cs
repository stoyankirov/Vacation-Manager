namespace VacationManager.API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
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

            var response = this._authService.Register(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("ConfirmRegistration")]
        public async Task<IActionResult> ConfirmRegistration(ConfirmRegistrationRequest request)
        {
            bool successfullyConfirmed = await this._authService.ConfirmRegistration(request);

            if (!successfullyConfirmed)
            {
                return Conflict(new Message(StatusCodes.Status409Conflict, MessageCode.IncorrectConfirmationCode));
            }

            return Ok();
        }
    }
}
