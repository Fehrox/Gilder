using Fluxor;

namespace Presentation.Store.Forecast;

public class ForecastFeature : Feature<ForecastState>
{
    public override string GetName() => nameof(ForecastFeature);

    protected override ForecastState GetInitialState() => new();
}