using Dapper;

using MediatR;

using TotalOne.Application.Services;
using TotalOne.Domain.Result;

namespace TotalOne.Application.Commands;

public record UpdateBusinessPartnerCommand(long BusinessPartnerId, string Name) : IRequest<CommandResult<UpdateBusinessPartnerCommandResult>>;

public record UpdateBusinessPartnerCommandResult(bool IsUpdated);

public class UpdateBusinessPartnerCommandHandler : IRequestHandler<UpdateBusinessPartnerCommand, CommandResult<UpdateBusinessPartnerCommandResult>>
{
    private readonly ITotalOneContext _totalOneContext;
    private readonly IArthausApiHttpService _arthouseApi;

    public UpdateBusinessPartnerCommandHandler(
        ITotalOneContext totalOneContext, 
        IArthausApiHttpService arthouseApi)
    {
        _totalOneContext = totalOneContext;
        _arthouseApi = arthouseApi;
    }

    public async Task<CommandResult<UpdateBusinessPartnerCommandResult>> Handle(UpdateBusinessPartnerCommand request, CancellationToken cancellationToken)
    {
        const string updateQuery = "UPDATE BusinessPartner SET Name = @Name WHERE BusinessPartnerId = @BusinessPartnerId";

        using var connection = _totalOneContext.CreateConnection();

        using var ts = TransactionScopeFactory.CreateReadCommittedTransactionScope();

        var affectedRows = await connection.ExecuteAsync(updateQuery, new
        {
            request.BusinessPartnerId,
            request.Name
        });


        ts.Complete();

        var isUpdated = affectedRows > 0;

        if (isUpdated)
        {
            var response = await _arthouseApi.UpdateBusinessPartner(request.BusinessPartnerId, request.Name, cancellationToken);
            return CommandResult.Success(new UpdateBusinessPartnerCommandResult(isUpdated));
        }

        return CommandResult.Failure<UpdateBusinessPartnerCommandResult>(ErrorType.BadRequest, "Business Partner was not successfully updated.");
    }
}
