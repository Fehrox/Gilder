namespace Presentation.Store.Forecast;

public record ForecastSetTransactionsCommand(IEnumerable<ForecastTransaction> Forecasts)
{ }