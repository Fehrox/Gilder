using System.Collections.Generic;
using Fluxor;
using Reconciler.Domain;

namespace Reconciler.Store
{
    public class TransactionsFeature : Feature<TransactionsState>
    {
        public override string GetName() => nameof(TransactionsFeature);
        protected override TransactionsState GetInitialState() => new(new List<TransactionViewModel>());
    }
}