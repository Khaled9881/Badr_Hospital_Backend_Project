using BadrHospital.Application.Interfaces;
using BadrHospital.Application.Services.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IRefreshTokenService refreshTokenService) : IRequestHandler<RefreshTokenCommand, AuthResult>
    {
        public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await refreshTokenService.RotateAsync(request.RefreshToken, cancellationToken);

            if (!result.Succeeded)
                throw new ValidationException(result.Error ?? "Invalid refresh token.");

            return new AuthResult(result.AccessToken!, result.RefreshToken!);
        }
    }
}
