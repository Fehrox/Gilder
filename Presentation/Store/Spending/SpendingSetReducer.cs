using Fluxor;

namespace Presentation.Store.Spending;

public class SpendingSetReducer : Reducer<SpendingState, SpendingSetCommand>
{
    public override SpendingState Reduce(SpendingState state, SpendingSetCommand action) => 
        new() {
            SpendingMonths = action.SpendingMonths,
            MaxMonthNet = action.MaxMonthNet,
        };
}