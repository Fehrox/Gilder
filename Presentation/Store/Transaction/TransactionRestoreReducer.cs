using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionRestoreReducer : Reducer<TransactionsState, TransactionRestoreAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionRestoreAction action)
        {
            var transactions = state.Transactions;
            transactions.AddRange(action.Transactions);
            transactions = transactions.OrderBy(t => t.Date).ToList();
            return new TransactionsState(transactions);
        }
    }
}