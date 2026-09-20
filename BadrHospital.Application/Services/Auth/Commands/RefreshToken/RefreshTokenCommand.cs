using BadrHospital.Application.Services.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResult>;
}
