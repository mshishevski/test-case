using MediatR;

using TotalOne.Application.Commands;

namespace TotalOne.Api.Mutations;

[ExtendObjectType("Mutation")]
public class BusinessPartnerMutation
{
    private readonly ISender _sender;

    public BusinessPartnerMutation(ISender sender)
    {
        _sender = sender;
    }

    public async Task<bool> CreateBusinessPartner(string name, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new CreateBusinessPartnerCommand(name), cancellationToken);
        return result.IsSuccess;
    }

    public async Task<bool> UpdateBusinessPartner(int businessPartnerId, string name, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateBusinessPartnerCommand(businessPartnerId, name), cancellationToken);
        return result.IsSuccess;
    }

    public async Task<bool> DeleteBusinessPartner(int businessPartnerId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteBusinessPartnerCommand(businessPartnerId), cancellationToken);
        return result.IsSuccess;
    }
}
