using System.Transactions;

namespace TotalOne.Application;

public static class TransactionScopeFactory
{
    public static TransactionScope CreateReadCommittedTransactionScope()
    {
        return new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}
