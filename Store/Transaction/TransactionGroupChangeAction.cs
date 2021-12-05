using Reconciler.Domain;

namespace Reconciler.Store
{
    public record TransactionGroupChangeAction(Hash TransactionHash, Hash GroupHash);
}