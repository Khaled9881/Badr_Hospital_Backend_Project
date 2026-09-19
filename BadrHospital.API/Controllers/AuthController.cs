using BadrHospital.Application.Services.Auth.Commands.ForgetPassword;
using BadrHospital.Application.Services.Auth.Commands.LogIn;
using BadrHospital.Application.Services.Auth.Commands.Register;
using BadrHospital.Application.Services.Auth.Commands.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadrHospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var token = await _mediator.Send(registerCommand);
            return Ok(token);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);
            if (result.isSignedInSuccessfully)
                return Ok(result.token);

            return Unauthorized();
        }

        [HttpPost("Forget_Password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand forgetPasswordCommand)
        {
            await _mediator.Send(forgetPasswordCommand);
            return Ok(new { message = "If an account with that email exists, a reset link has been sent." });
        }

        [HttpPost("Reset_Password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand)
        {
            await _mediator.Send(resetPasswordCommand);
            return Ok();
        }


    }
}
