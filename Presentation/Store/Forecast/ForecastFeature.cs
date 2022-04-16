using Fluxor;

namespace Presentation.Store.Forecast;

public class ForecastFeature : Feature<ForecastState>
{
    public override string GetName() => nameof(ForecastFeature);

    protected override ForecastState GetInitialState()
    {
        const double DAYS_IN_YEAR = 365.242199;
        const double FORECAST_DAYS = 3 * DAYS_IN_YEAR;
        return new ForecastState() {
            Till = DateTime.Now + TimeSpan.FromDays(FORECAST_DAYS),
            Transactions = new List<ForecastTransaction>()
        };
    }
}