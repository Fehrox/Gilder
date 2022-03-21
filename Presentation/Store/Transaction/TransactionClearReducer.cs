using Fluxor;

namespace Presentation.Store.Transaction;

public class TransactionClearReducer : Reducer<TransactionsState, TransactionClearAction>
{
    public override TransactionsState Reduce(TransactionsState state, TransactionClearAction action)
    {
        return new TransactionsState(new List<Domain.Transaction>());
    }
}