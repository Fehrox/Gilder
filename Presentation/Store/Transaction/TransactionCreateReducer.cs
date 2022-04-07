using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionCreateReducer : Reducer<TransactionState, TransactionCreateAction>
    {
        public override TransactionState Reduce(TransactionState state, TransactionCreateAction action)
        {
            var transactions = state.Transactions;
            transactions.AddRange(action.Transactions);
            transactions = transactions.OrderBy(t => t.Date).ToList();
            return new TransactionState(transactions);
        }
    }
}