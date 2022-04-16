using Fluxor;

namespace Presentation.Store.Forecast;

public class ForecastSetReducer : Reducer<ForecastState, ForecastSetTransactionsCommand>
{
    public override ForecastState Reduce(ForecastState state, ForecastSetTransactionsCommand action)
        => state with {
            Transactions = action.Forecasts
        };
}