using Dapper;

using MediatR;

using TotalOne.Domain.Result;

namespace TotalOne.Application.Commands;

public record CreateBusinessPartnerCommand(string Name) : IRequest<CommandResult<CreateBusinessPartnerCommandResult>>;

public record CreateBusinessPartnerCommandResult(int BusinessPartnerId);

public class CreateBusinessPartnerCommandHandler : IRequestHandler<CreateBusinessPartnerCommand, CommandResult<CreateBusinessPartnerCommandResult>>
{
    private readonly ITotalOneContext _totalOneContext;

    public CreateBusinessPartnerCommandHandler(ITotalOneContext totalOneContext)
    {
        _totalOneContext = totalOneContext;
    }

    public async Task<CommandResult<CreateBusinessPartnerCommandResult>> Handle(CreateBusinessPartnerCommand request, CancellationToken cancellationToken)
    {
        using var ts = TransactionScopeFactory.CreateReadCommittedTransactionScope();

        const string baseQuery = @"
            INSERT INTO BusinessPartners (Name, LastUpdate)
            VALUES (@Name, @LastUpdate)
            RETURNING BusinessPartnerId;";

        using var connection = _totalOneContext.CreateConnection();

        var parameters = new
        {
            Name = request.Name,
            LastUpdate = DateTime.UtcNow
        };

        var businessPartnerId = await connection.ExecuteScalarAsync<int>(baseQuery, parameters);

        ts.Complete();

        return CommandResult.Success(new CreateBusinessPartnerCommandResult(businessPartnerId));

        
    }
}