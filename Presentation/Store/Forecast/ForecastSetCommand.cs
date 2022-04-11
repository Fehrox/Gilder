namespace Presentation.Store.Forecast;

public record ForecastSetCommand(IEnumerable<MonthForecast> Forecasts)
{ }