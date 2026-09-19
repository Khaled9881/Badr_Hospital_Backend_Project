using BadrHospital.Application.Common.Exceptions;
using BadrHospital.Application.Interfaces;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler(IIdentityService identityService) : IRequestHandler<ForgetPasswordCommand, string>
    {
        public async Task<string> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.GetUserIdByEmailAsync(request.Email);
            if (userId == Guid.Empty)
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Email", "Invalid Email") });

            return await identityService.ForgetPassword(userId);
        }
    }
}
