using Fluxor;

namespace Presentation.Store.Forecast;

public class ForecastSetReducer : Reducer<ForecastState, ForecastSetCommand>
{
    public override ForecastState Reduce(ForecastState state, ForecastSetCommand action) 
        => new() { MonthForecasts = action.Forecasts };
}