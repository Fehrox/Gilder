using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetFeature : Feature<BudgetState>
{
    public override string GetName() => nameof(BudgetFeature);

    protected override BudgetState GetInitialState() => new(new List<Domain.Budget>());
}