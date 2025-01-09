using FluentValidation;
using Zaczytani.Application.Shared.Commands;
using Zaczytani.Domain.Constants;

namespace Zaczytani.Application.Shared.Validators;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(dto => dto.CurrentPassword)
           .NotEmpty().WithMessage("Current password cannot be empty.");

        RuleFor(dto => dto.NewPassword)
            .NotEmpty().WithMessage("New password cannot be empty.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters long.")
            .Matches(Resources.PasswordRegex).WithMessage("New password must include special characters and numbers");
    }
}