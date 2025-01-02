using FluentValidation;

using TotalOne.Application.Queries;

namespace TotalOne.Application.Validators.Queries;

public class GetAllBusinessPartnersQueryValidator : AbstractValidator<GetAllBusinessPartnersQuery>
{
    public GetAllBusinessPartnersQueryValidator()
    {
        RuleFor(x => x.PageIndex)
                .GreaterThan(-1)
                .WithMessage("PageIndex should not be null or a negative integer.");

        RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize should not be null or a negative integer.");
    }
}
