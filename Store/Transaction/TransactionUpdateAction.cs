using Reconciler.Domain;

namespace Reconciler.Store
{
    public record TransactionUpdateAction(Transaction Transaction, Group UpdatedGroup, string UpdatedNote);
}