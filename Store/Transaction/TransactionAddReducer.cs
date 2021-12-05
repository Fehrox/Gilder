using System.Linq;
using Fluxor;

namespace Reconciler.Store
{
    public class TransactionAddReducer : Reducer<TransactionsState, TransactionAddAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionAddAction action)
        {
            var groupViewModel = action.Group != null 
                ? new GroupViewModel {
                    Name = action.Group.GroupName,
                } : null;
            
            var transactionViewModel = new TransactionViewModel {
                Hash = action.Transaction.ToHash(),
                Charge = action.Transaction.Charge,
                Class = action.Transaction.Class,
                Details = action.Transaction.Details,
                Date = action.Transaction.Date,
                Group = groupViewModel
            };

            var newTransactions = new[] { transactionViewModel };
            var expandedTransactions = state.Transactions.Concat(newTransactions);

            return new TransactionsState(expandedTransactions);
        }
    }
}