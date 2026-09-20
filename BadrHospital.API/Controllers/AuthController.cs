using BadrHospital.Application.Services.Auth.Commands.ChangePassword;
using BadrHospital.Application.Services.Auth.Commands.ForgetPassword;
using BadrHospital.Application.Services.Auth.Commands.LogIn;
using BadrHospital.Application.Services.Auth.Commands.RefreshToken;
using BadrHospital.Application.Services.Auth.Commands.Register;
using BadrHospital.Application.Services.Auth.Commands.ResetPassword;
using BadrHospital.Application.Services.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            if (!result.isSignedInSuccessfully)
                return Unauthorized();

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new { accessToken = result.token });
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

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResult>> RefreshToken(RefreshTokenCommand command)
        {
            var rawToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(rawToken))
                return Unauthorized();

            var result = await _mediator.Send(new RefreshTokenCommand(rawToken));

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new { accessToken = result.AccessToken });
        }

        [HttpPost("Change_Password")]
        [Authorize]
        public async Task<IActionResult> ChangePassowrd([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token: missing user identifier.");

            var command = new ChangePasswordCommand(userId, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            await _mediator.Send(command);
            return Ok("Password Changed Successfully.");
        }


    }
}
