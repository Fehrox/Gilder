using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetCreateReducer : Reducer<BudgetState, BudgetCreateAction>
{
    public override BudgetState Reduce(BudgetState state, BudgetCreateAction action) 
        => new(action.Budgets.Concat(action.Budgets));
}