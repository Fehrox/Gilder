using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionUpdateReducer : Reducer<DeductionState, DeductionUpdateAction>
{
    public override DeductionState Reduce(DeductionState state, DeductionUpdateAction action)
    {
        var deductionList = state.Deductions;
        
        var deduction = state.Deductions
            .First(d => d.Id == action.Deduction.Id);

        deduction.Reason = action.Deduction.Reason;
        
        return new DeductionState {
            Deductions = deductionList.ToList()
        };
    }
}