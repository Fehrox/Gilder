using System.Collections.Generic;
using Fluxor;
using Reconciler.Domain;

namespace Reconciler.Store
{
    public class TransactionUpdateReducer : Reducer<TransactionsState, TransactionUpdateAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionUpdateAction action)
        {
            var updatedTransactions = new List<TransactionViewModel>();
            var transactions = state.Transactions;
            foreach (var transaction in transactions) {
                if (transaction.Hash.ToString() == action.Transaction.ToHash().ToString()) {
                    var updated = transaction with {
                        Hash = action.Transaction.ToHash(),
                        Charge = action.Transaction.Charge,
                        Class = action.Transaction.Class,
                        Date = action.Transaction.Date,
                        Details = action.Transaction.Details,
                        Note = action.UpdatedNote,
                        Group = new GroupViewModel {
                            Name = action.UpdatedGroup?.GroupName,
                        }
                    };
                    updatedTransactions.Add(updated);
                } else {
                    updatedTransactions.Add(transaction);
                }
            }
            
            return new TransactionsState(updatedTransactions);
        }
    }
}