using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionFeature : Feature<TransactionState>
    {
        public override string GetName() => nameof(TransactionFeature);
        protected override TransactionState GetInitialState() => new(new List<Domain.Transaction>());
    }
}