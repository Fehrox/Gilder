using Fluxor;

namespace Presentation.Store.Budget;

public class BudgetClearReducer : Reducer<BudgetState, BudgetClearAction>
{
    public override BudgetState Reduce(BudgetState state, BudgetClearAction action) 
        => new(new List<Domain.Budget>());
}