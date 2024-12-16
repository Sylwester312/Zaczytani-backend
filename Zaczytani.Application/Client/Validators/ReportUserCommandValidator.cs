using FluentValidation;
using Zaczytani.Application.Client.Commands;

namespace Zaczytani.Application.Client.Validators;

public class ReportUserCommandValidator : AbstractValidator<ReportUserCommand>
{
    public ReportUserCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content cannot be empty.")
            .MinimumLength(5).WithMessage("Content must be at least 5 characters long.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid category selected.");

        RuleFor(x => x.ReviewId)
            .NotEmpty().WithMessage("ReviewId cannot be empty.")
            .Must(id => id != Guid.Empty).WithMessage("ReviewId must be a valid GUID.");
    }
}