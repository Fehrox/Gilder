using Domain;

namespace Presentation.Store
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