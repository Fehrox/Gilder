using Fluxor;

namespace Presentation.Store.Transaction;

public record TransactionCreateAction (IEnumerable<Domain.Transaction> Transactions);

public class TransactionCreateReducer : Reducer<TransactionState, TransactionCreateAction>
{
    public override TransactionState Reduce(TransactionState state, TransactionCreateAction action)
        => state with {
            Transactions = state.Transactions.Concat(action.Transactions)
        };
}

