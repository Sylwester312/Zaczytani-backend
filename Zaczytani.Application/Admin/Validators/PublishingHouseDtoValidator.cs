using FluentValidation;
using Zaczytani.Application.Dtos;

namespace Zaczytani.Application.Admin.Validators;

public class PublishingHouseDtoValidator : AbstractValidator<PublishingHouseDto>
{
    public PublishingHouseDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Publishing house name is required.")
            .MaximumLength(150).WithMessage("Publishing house name cannot exceed 150 characters.");
    }
}
