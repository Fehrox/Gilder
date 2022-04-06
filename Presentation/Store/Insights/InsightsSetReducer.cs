using Fluxor;

namespace Presentation.Store.Insights;

public class InsightsSetReducer : Reducer<InsightsState, InsightsSetCommand>
{
    public override InsightsState Reduce(InsightsState state, InsightsSetCommand action) => 
        new() {
            MonthInsights = action.MonthInsights,
            MaxMonthNet = action.MaxMonthNet,
            
        };
}