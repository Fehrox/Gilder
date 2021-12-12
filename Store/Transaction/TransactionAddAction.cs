using Reconciler.Domain;

namespace Reconciler.Store
{
    public record TransactionAddAction(Transaction Transaction, Group Group, string Note);
}