using Fluxor;

namespace Reconciler.Store
{
    public class TransactionRestoreReducer : Reducer<TransactionsState, TransactionRestoreAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionRestoreAction action)
        {
            var transactions = state.Transactions;
            transactions.Add(action.Transaction);
            return new TransactionsState(transactions);
        }
    }
}