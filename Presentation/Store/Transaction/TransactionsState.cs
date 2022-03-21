namespace Presentation.Store.Transaction
{
    public class TransactionsState
    {
        public TransactionsState(IEnumerable<Domain.Transaction> transactions)
        {
            Transactions = transactions.ToList();
        }

        public List<Domain.Transaction> Transactions { get; }
    }
}