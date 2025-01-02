using FluentValidation;
using TotalOne.Application.Queries;

namespace TotalOne.Application.Validators.Queries;

public class FilterBusinessPartnerQueryValidator : AbstractValidator<FilterBusinessPartnersQuery>
{
    public FilterBusinessPartnerQueryValidator()
    {
        RuleFor(x => x.BusinessPartnerId)
            .GreaterThan(0)
            .When(x => x.BusinessPartnerId.HasValue)
            .WithMessage("Business Partner Id must be greater than 0.");

        RuleFor(x => x.LastUpdateStart)
            .LessThanOrEqualTo(x => x.LastUpdateEnd)
            .When(x => x.LastUpdateStart.HasValue && x.LastUpdateEnd.HasValue)
            .WithMessage("Start date must be before or equal to end date.");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("Name cannot exceed 100 characters.");
    }
}
