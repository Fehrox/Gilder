using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetDeleteReducer : Reducer<BudgetState, BudgetDeleteAction> {
    public override BudgetState Reduce(BudgetState state, BudgetDeleteAction action)
    {
        return state with
        {
            Budgets = state.Budgets.Except(new[] {action.Budget})
        };
    }
}