using BadrHospital.Application.Services.Auth.Commands.LogIn;
using BadrHospital.Application.Services.Auth.Commands.Register;
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
        public async Task<IActionResult> LogIn(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);
            if (result.isSignedInSuccessfully)
                return Ok(result.token);

            return Unauthorized();
        }


    }
}
