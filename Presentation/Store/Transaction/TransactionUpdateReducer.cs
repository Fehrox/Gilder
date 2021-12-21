using Domain;
using Fluxor;

namespace Presentation.Store
{
    public class TransactionUpdateReducer : Reducer<TransactionsState, TransactionUpdateAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionUpdateAction action)
        {
            var updatedTransactions = new List<Transaction>();
            var transactions = state.Transactions;
            foreach (var transaction in transactions) {
                if (transaction.Id == action.Transaction.Id) {
                    updatedTransactions.Add(action.Transaction);
                } else {
                    updatedTransactions.Add(transaction);
                }
            }
            
            return new TransactionsState(updatedTransactions);
        }
    }
}