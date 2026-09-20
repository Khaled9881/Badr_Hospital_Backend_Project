using BadrHospital.Application.Common;
using BadrHospital.Application.Interfaces;
using BadrHospital.Application.Services.Auth.DTOs;
using MediatR;

namespace BadrHospital.Application.Services.Auth.Commands.LogIn
{
    public class LoginCommandHandler(IIdentityService identityService, IJWTService jWTService, IRefreshTokenService refreshTokenService) : IRequestHandler<LoginCommand, LoginResultDTO>
    {
        public async Task<LoginResultDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var (isSignedInSuccessfully, id, roles) = await identityService.SigninAsync(request.Email, request.Password);

            if (!isSignedInSuccessfully)
            {
                return new LoginResultDTO()
                {
                    isSignedInSuccessfully = isSignedInSuccessfully,
                    FailureMessage = "Invalid email or password."
                };
            }

            var accessToken = jWTService.CreateToken(
                new TokenRequest()
                {
                    UserId = id.ToString(),
                    Email = request.Email,
                    Roles = roles
                });

            var refreshToken = await refreshTokenService.GenerateAsync(id, cancellationToken);


            return new LoginResultDTO()
            {
                isSignedInSuccessfully = isSignedInSuccessfully,
                token = accessToken,
                RefreshToken = refreshToken
            };

        }
    }
}
