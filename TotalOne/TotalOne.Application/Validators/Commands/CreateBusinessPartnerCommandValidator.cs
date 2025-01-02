using FluentValidation;

using TotalOne.Application.Commands;

namespace TotalOne.Application.Validators.Commands;

public class CreateBusinessPartnerCommandValidator : AbstractValidator<CreateBusinessPartnerCommand>
{
    public CreateBusinessPartnerCommandValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name property should not be empty.");

    }
}
