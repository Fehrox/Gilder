using Reconciler.Domain;

namespace Reconciler.Store
{
    public record TransactionNoteChangeAction(Hash TransactionHash, string Note);
}