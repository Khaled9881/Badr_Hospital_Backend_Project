using BadrHospital.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using BadrHospital.Application.Common.Exceptions;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace BadrHospital.Application.Services.Auth.Commands.Register
{
    public class RegisterCommandHandler(IIdentityService identityService, IJWTService jWTService) : IRequestHandler<RegisterCommand, string>
    {
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await identityService.FindByEmailAsync(request.Email))
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Email", "Email is Already Existed") }
                );
            if (await identityService.FindByNameAsync(request.userName))
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("UserName", "User name is already taken") });

            var (Result, UserId) = await identityService.CreateUserAsync(request.Email, request.Password, request.userName, request.PhoneNumber);

            if (!Result.Succeeded)
                ThrowValidationException(Result);

            var AddtoRoleResult = await identityService.AddtoRoleAsync(UserId.ToString(), request.Role);
            if (!AddtoRoleResult.Succeeded)
                ThrowValidationException(Result);

            return jWTService.CreateToken(new Common.TokenRequest()
            {
                UserId = UserId.ToString(),
                Email = request.Email,
                Roles = new List<string>() { request.Role }
            });


        }

        // Helper Function
        [DoesNotReturn]
        private void ThrowValidationException(IdentityResult result)
        {
            var errors = string.Join(" ", result.Errors.Select(e => e.Description));
            throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Errors", errors) });
        }
    }
}
