using System.Collections.Generic;
using Fluxor;
using Reconciler.Domain;

namespace Reconciler.Store
{
    public class TransactionNoteChangeReducer : Reducer<TransactionsState, TransactionNoteChangeAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionNoteChangeAction action)
        {
            var updatedTransactions = new List<TransactionViewModel>();
            var transactions = state.Transactions;
            foreach (var transaction in transactions) {
                if (transaction.Hash.ToString() == action.TransactionHash.ToString()) {
                    var updated = transaction with {
                        Note = action.Note
                    };
                    // var updated = transaction with {
                    //     Hash = action.Transaction.ToHash(),
                    //     Charge = action.Transaction.Charge,
                    //     Class = action.Transaction.Class,
                    //     Date = action.Transaction.Date,
                    //     Details = action.Transaction.Details,
                    //     Group = new GroupViewModel {
                    //         Name = transaction.Group.Name,
                    //     }
                    // };
                    updatedTransactions.Add(updated);
                } else {
                    updatedTransactions.Add(transaction);
                }
            }
            
            return new TransactionsState(updatedTransactions);
        }
    }
}