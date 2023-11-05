using Domain;
using Fluxor;

namespace Presentation.Store.Deductions;

public record DeductionClearAction { }

public class DeductionClearReducer : Reducer<DeductionState, DeductionClearAction>
{
    public override DeductionState Reduce(DeductionState state, DeductionClearAction action) => new ();
}