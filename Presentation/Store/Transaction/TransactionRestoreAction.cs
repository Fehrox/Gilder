using Domain;

namespace Presentation.Store
{
    public record TransactionRestoreAction(IEnumerable<Transaction> Transactions);
}