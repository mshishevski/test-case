using FluentValidation;
using TotalOne.Application.Commands;

namespace TotalOne.Application.Validators.Commands;

public class DeleteBusinessPartnerCommandValidator : AbstractValidator<DeleteBusinessPartnerCommand>
{
    public DeleteBusinessPartnerCommandValidator()
    {
        RuleFor(x => x.BusinessPartnerId)
                .NotEmpty()
                .WithMessage("BusinessPartnerId should not be empty.");
    }
}
