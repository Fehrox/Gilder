using System.Collections.Generic;
using Fluxor;
using Reconciler.Domain;

namespace Reconciler.Store
{
    public class TransactionGroupChangeReducer : Reducer<TransactionsState, TransactionGroupChangeAction>
    {
        public override TransactionsState Reduce(TransactionsState state, TransactionGroupChangeAction action)
        {
            var updatedTransactions = new List<TransactionViewModel>();
            var transactions = state.Transactions;
            foreach (var transaction in transactions) {
                if (transaction.Hash.ToString() == action.TransactionHash.ToString()) { 
                    var updated = transaction with {
                        Group = new GroupViewModel {
                            Name = transaction.Group.Name,
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