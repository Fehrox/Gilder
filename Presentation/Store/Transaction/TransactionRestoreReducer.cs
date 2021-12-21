using Fluxor;

namespace Presentation.Store
{
    public class TransactionRestoreReducer : Reducer<TransactionsState, TransactionRestoreAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionRestoreAction action)
        {
            var transactions = state.Transactions;
            transactions.Add(action.Transaction);
            transactions = transactions.OrderBy(t => t.Date).ToList();
            return new TransactionsState(transactions);
        }
    }
}