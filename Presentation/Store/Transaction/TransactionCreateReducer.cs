using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionCreateReducer : Reducer<TransactionState, TransactionCreateAction>
    {
        public override TransactionState Reduce(TransactionState state, TransactionCreateAction action)
            => new(state.Transactions.Concat(action.Transactions));
    }
}