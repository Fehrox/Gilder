using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionUpdateReducer : Reducer<TransactionState, TransactionUpdateAction>
    {
        public override TransactionState Reduce(TransactionState state, TransactionUpdateAction action)
        {
            var updatedTransactions = new List<Domain.Transaction>();
            var transactions = state.Transactions;
            foreach (var transaction in transactions) {
                if (transaction.Id == action.Transaction.Id) {
                    updatedTransactions.Add(action.Transaction);
                } else {
                    updatedTransactions.Add(transaction);
                }
            }
            
            return new TransactionState(updatedTransactions);
        }
    }
}