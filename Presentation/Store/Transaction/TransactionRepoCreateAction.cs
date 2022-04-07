namespace Presentation.Store.Transaction;

public class TransactionRepoCreateAction
{
    public IEnumerable<Domain.Transaction> Transactions { get; set; }
}