using Fluxor;

namespace Presentation.Store.Forecast;

public class ForecastSetTillReducer : Reducer<ForecastState, ForecastSetTillCommand>
{
    public override ForecastState Reduce(ForecastState state, ForecastSetTillCommand action)
    {
        return state with {
            Till = action.Till
        };
    }
}