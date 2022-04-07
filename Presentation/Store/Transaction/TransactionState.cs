namespace Presentation.Store.Transaction
{
    public class TransactionState
    {
        public TransactionState(IEnumerable<Domain.Transaction> transactions)
        {
            Transactions = transactions.ToList();
        }

        public List<Domain.Transaction> Transactions { get; }
    }
}