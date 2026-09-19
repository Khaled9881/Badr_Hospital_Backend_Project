using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(string email, string resetToken, string newPassword) : IRequest;
}
