using BadrHospital.Application.Common.Exceptions;
using BadrHospital.Application.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace BadrHospital.Application.Services.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler(IIdentityService identityService, IRefreshTokenService refreshTokenService) : IRequestHandler<ChangePasswordCommand>
    {
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.ChangePasswordAsync(
             request.UserId, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
                throw new ValidationException(new List<ValidationFailure>
            {
                new("Password", string.Join(" ", result.Errors.Select(e => e.Description)))
            });

            await refreshTokenService.RevokeAllForUserAsync(Guid.Parse(request.UserId), cancellationToken);
        }
    }
}
