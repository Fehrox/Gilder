namespace Presentation.Store.Forecast;

public record ForecastSetCommand(IEnumerable<ForecastTransaction> Forecasts)
{ }