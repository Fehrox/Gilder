using Reconciler.Domain;

namespace Reconciler.Store
{
    public record TransactionGroupChangeAction(Hash TransctionHash, Hash GroupHash);
}