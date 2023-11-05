using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionCreateReducer : Reducer<DeductionState, DeductionCreateAction>
{
    public override DeductionState Reduce(DeductionState state, DeductionCreateAction action)
    {
        var oldWithCreatedDeductions = state.Deductions
            .Concat(action.Deductions);
        
        return new DeductionState {
            Deductions = oldWithCreatedDeductions.ToList()
        };
    }
}