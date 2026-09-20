using BadrHospital.Application.Common.Exceptions;
using BadrHospital.Application.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace BadrHospital.Application.Services.Auth.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler(IIdentityService identityService, IConfiguration config, IEmailService emailService) : IRequestHandler<ForgetPasswordCommand>
    {
        public async Task Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.GetUserIdByEmailAsync(request.Email);
            if (userId == Guid.Empty)
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Email", "Invalid Email") });

            var token = await identityService.ForgetPassword(userId);
            //var encodedToken = WebUtility.UrlEncode(token);
            //var resetLink = $"{config["ClientApp:ResetPasswordUrl"]}?email={WebUtility.UrlEncode(request.Email)}&token={token}";

            //await emailService.SendAsync(request.Email, "Reset your password",
            //    $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>");

            // Using this Approach for Testing Purpises !!!!!!!!!!!!!!!!!!!!!!
            await emailService.SendAsync(request.Email, "Reset your password",
                $"<p>Here are the Reset Token \n{token}</p>");
        }
    }
}
