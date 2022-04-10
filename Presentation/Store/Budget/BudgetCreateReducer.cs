using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetCreateReducer : Reducer<BudgetState, BudgetCreateAction>
{
    public override BudgetState Reduce(BudgetState state, BudgetCreateAction action) 
        => new(state.Budgets.Concat(action.Budgets));
}