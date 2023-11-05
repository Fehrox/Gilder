using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionLoadedReducer : Reducer<DeductionState, DeductionLoadedAction>
{
    public override DeductionState Reduce(DeductionState state, DeductionLoadedAction action)
    {
        return new DeductionState {
            Deductions = action.Deduction.ToList()
        };
    }
}