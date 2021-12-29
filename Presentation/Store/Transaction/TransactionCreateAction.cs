using Domain;

namespace Presentation.Store
{
    public record TransactionCreateAction (IEnumerable<Transaction> Transactions);
}