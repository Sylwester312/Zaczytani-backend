using FluentValidation;
using Zaczytani.Application.Admin.Commands;

namespace Zaczytani.Application.Admin.Validators;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Book title is required.")
            .MinimumLength(2).WithMessage("Book title must be at least 2 characters long.");
    }
}
