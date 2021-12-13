using Reconciler.Domain;

namespace Reconciler.Store
{
    public record TransactionGroupChangeAction{
        public Hash GroupHash { get; set; }
        public Hash TransactionHash { get; set; }
    }
}