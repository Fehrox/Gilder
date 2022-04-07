using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionRestoreReducer : Reducer<TransactionState, TransactionRestoreAction>
    {
        public override TransactionState Reduce(TransactionState state, TransactionRestoreAction action)
        {
            var transactions = state.Transactions;
            transactions.AddRange(action.Transactions);
            transactions = transactions.OrderBy(t => t.Date).ToList();
            return new TransactionState(transactions);
        }
    }
}