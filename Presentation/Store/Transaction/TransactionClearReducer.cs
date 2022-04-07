using Fluxor;

namespace Presentation.Store.Transaction;

public class TransactionClearReducer : Reducer<TransactionState, TransactionClearAction>
{
    public override TransactionState Reduce(TransactionState state, TransactionClearAction action)
    {
        return new TransactionState(new List<Domain.Transaction>());
    }
}