using System.Collections.Generic;
using System.Linq;
using Reconciler.Domain;

namespace Reconciler.Store
{
    public class TransactionsState
    {
        public TransactionsState(IEnumerable<TransactionViewModel> transactions)
        {
            Transactions = transactions.ToList();
        }

        public List<TransactionViewModel> Transactions { get; }
    }
}