using FluentValidation;
using Zaczytani.Application.Dtos;

namespace Zaczytani.Application.Admin.Validators;

public class AuthorDtoValidator : AbstractValidator<AuthorDto>
{
    public AuthorDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Author ID cannot be null.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Author name is required.")
            .MinimumLength(2).WithMessage("Author name must be at least 2 characters long.")
            .MaximumLength(150).WithMessage("Author name cannot exceed 150 characters.");
    }
}
