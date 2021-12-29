using Fluxor;

namespace Presentation.Store
{
    public class TransactionCreateReducer : Reducer<TransactionsState, TransactionCreateAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionCreateAction action)
        {
            var transactions = state.Transactions;
            transactions.AddRange(action.Transactions);
            transactions = transactions.OrderBy(t => t.Date).ToList();
            return new TransactionsState(transactions);
        }
    }
}