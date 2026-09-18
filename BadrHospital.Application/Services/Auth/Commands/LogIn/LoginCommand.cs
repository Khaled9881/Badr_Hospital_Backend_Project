using BadrHospital.Application.Services.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.LogIn
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResultDTO>;
}
