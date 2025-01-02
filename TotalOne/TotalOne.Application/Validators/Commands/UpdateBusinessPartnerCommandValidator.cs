using FluentValidation;
using TotalOne.Application.Commands;

namespace TotalOne.Application.Validators.Commands;

public class UpdateBusinessPartnerCommandValidator : AbstractValidator<UpdateBusinessPartnerCommand>
{
    public UpdateBusinessPartnerCommandValidator()
    {
        RuleFor(x => x.BusinessPartnerId)
                .NotEmpty()
                .WithMessage("BusinessPartnerId should not be empty.");
    }
}
