namespace VacationManager.API.Controllers
{
    using FluentResult;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Domain.Enums;
    using VacationManager.Domain.Models;
    using VacationManager.Domain.Requests;
    using VacationManager.Domain.Responses;

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
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            bool userExists = await this._authService.UserExists(request.Email);

            if (userExists)
            {
                return Conflict(new Message(StatusCodes.Status409Conflict, MessageCode.UserExists));
            }

            var response = this._authService.Register(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("ConfirmRegistration")]
        public Task<IActionResult> ConfirmRegistration(ConfirmRegistrationRequest request)
            => this._authService.ConfirmRegistration(request).ToActionResultAsync(this);

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Login([Required] LoginRequest request)
            => this._authService.Login(request).ToActionResultAsync(this);
    }
}
