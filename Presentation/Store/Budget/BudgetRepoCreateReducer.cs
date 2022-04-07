using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetRepoCreateReducer : Reducer<BudgetState, BudgetRepoCreateAction>
{
    public override BudgetState Reduce(BudgetState state, BudgetRepoCreateAction action)
    {
        return state with {
            Budgets = state.Budgets.Concat(action.Budgets) 
        };
    }
}