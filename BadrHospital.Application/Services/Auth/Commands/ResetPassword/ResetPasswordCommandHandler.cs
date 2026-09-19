using BadrHospital.Application.Common.Exceptions;
using BadrHospital.Application.Interfaces;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler(IIdentityService identityService) : IRequestHandler<ResetPasswordCommand>
    {

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.GetUserIdByEmailAsync(request.email);
            if (userId == Guid.Empty)
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Email", "Invalid Email") });

            var result = await identityService.ResetPassword(userId, request.resetToken, request.newPassword);

            if (!result.Succeeded)
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Reset", "Password Reset Failed") });


        }
    }
}
