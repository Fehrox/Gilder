using System.Collections.Generic;
using System.Linq;
using Reconciler.Domain;

namespace Reconciler.Store
{
    public class TransactionsState
    {
        public TransactionsState(IEnumerable<Transaction> transactions)
        {
            Transactions = transactions.ToList();
        }

        public List<Transaction> Transactions { get; }
    }
}