namespace Presentation.Store.Forecast;

public record ForecastState
{
    public IEnumerable<ForecastTransaction> ForecastTransactions { get; set; } = Array.Empty<ForecastTransaction>();
}

public class ForecastTransaction
{
    public string Details { get; set; }
    public Domain.Group? Group { get; set; }
    public DateTime Date { get; set; }
    public double Charge { get; set; }
}