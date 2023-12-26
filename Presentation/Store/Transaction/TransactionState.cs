namespace Presentation.Store.Transaction;

public record TransactionState
{
    public IEnumerable<Domain.Transaction> Transactions { get; set; } = new List<Domain.Transaction>();
}