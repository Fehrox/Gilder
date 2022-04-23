namespace Presentation.Store.Transaction
{
    public record TransactionCsvLoadAction(IEnumerable<Domain.Transaction> Transactions);
}