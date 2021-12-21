using Domain;
using Fluxor;

namespace Presentation.Store
{
    public class TransactionsFeature : Feature<TransactionsState>
    {
        public override string GetName() => nameof(TransactionsFeature);
        protected override TransactionsState GetInitialState() => new(new List<Transaction>());
    }
}