namespace Presentation.Store.Forecast;

public record ForecastState
{
    public IEnumerable<MonthForecast> MonthForecasts { get; set; } = Array.Empty<MonthForecast>();
}

public record MonthForecast
{
    public DateTime Month { get; set; }
    public double NetPosition { get; set; }
}