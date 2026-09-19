using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.ForgetPassword
{
    public record ForgetPasswordCommand(string Email) : IRequest<string>;
}
