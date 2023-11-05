using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionRemoveReducer : Reducer<DeductionState, DeductionRemoveAction>
{
    public override DeductionState Reduce(DeductionState state, DeductionRemoveAction action)
    {
        var deductionList = state.Deductions
            .Where(d => d.Id != action.Deduction.Id);
        return new DeductionState {
            Deductions = deductionList.ToList()
        };
    }
}