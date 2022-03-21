namespace Presentation.Store.Transaction
{
    public record TransactionRestoreAction(IEnumerable<Domain.Transaction> Transactions);
}