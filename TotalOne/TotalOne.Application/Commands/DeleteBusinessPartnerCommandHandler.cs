using Dapper;

using MediatR;
using TotalOne.Domain.Result;

namespace TotalOne.Application.Commands;

public record DeleteBusinessPartnerCommand(long BusinessPartnerId) : IRequest<CommandResult<DeleteBusinessPartnerCommandResult>>;

public record DeleteBusinessPartnerCommandResult(bool IsDeleted);

public class DeleteBusinessPartnerCommandHandler : IRequestHandler<DeleteBusinessPartnerCommand, CommandResult<DeleteBusinessPartnerCommandResult>>
{
    private readonly ITotalOneContext _totalOneContext;

    public DeleteBusinessPartnerCommandHandler(ITotalOneContext totalOneContext)
    {
        _totalOneContext = totalOneContext;
    }

    public async Task<CommandResult<DeleteBusinessPartnerCommandResult>> Handle(DeleteBusinessPartnerCommand request, CancellationToken cancellationToken)
    {
        const string deleteQuery = "DELETE FROM BusinessPartner WHERE BusinessPartnerId = @BusinessPartnerId";

        using var connection = _totalOneContext.CreateConnection();

        var affectedRows = await connection.ExecuteAsync(deleteQuery, new { request.BusinessPartnerId });

        var isDeleted = affectedRows > 0;

        return CommandResult.Success(new DeleteBusinessPartnerCommandResult(isDeleted));
    }
}
