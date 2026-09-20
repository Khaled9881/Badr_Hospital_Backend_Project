using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(string UserId, string CurrentPassword, string NewPassword) : IRequest;
}
