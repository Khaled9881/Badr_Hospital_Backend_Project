using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User id is required.")
                .Must(BeAValidGuid).WithMessage("User id must be a valid GUID.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
                .NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from the current password.");
        }

        private static bool BeAValidGuid(string value)
            => Guid.TryParse(value, out _);
    }
}
