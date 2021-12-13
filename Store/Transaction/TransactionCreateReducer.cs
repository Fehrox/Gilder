using System.Linq;
using Fluxor;

namespace Reconciler.Store
{
    public class TransactionCreateReducer : Reducer<TransactionsState, TransactionCreateAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionCreateAction action)
        {
            var transactions = state.Transactions;
            transactions.Add(action.Transaction);
            return new TransactionsState(transactions);
        }
    }
}