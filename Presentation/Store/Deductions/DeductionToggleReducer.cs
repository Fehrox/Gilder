using Domain;
using Fluxor;

namespace Presentation.Store.Deductions;


public class DeductionToggleReducer : Reducer<DeductionState, DeductionToggleAction>
{
    public override DeductionState Reduce(DeductionState state, DeductionToggleAction action)
    {
        var deductionList = state.Deductions;
        
        var deduction = state.Deductions
            .FirstOrDefault(a => a.TransactionId == action.Transaction.Id);

        if (deduction != null)
            deductionList.Remove(deduction);
        else 
            deductionList.Add(new Deduction {
                TransactionId = action.Transaction.Id,
                Id = Guid.NewGuid()
            });
        
        return new DeductionState {
            Deductions = deductionList.ToList()
        };
    }
}