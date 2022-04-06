namespace Presentation.Store.Insights;

public record InsightsState
{
    public IEnumerable<InsightMonth> MonthInsights { get; set; } = Array.Empty<InsightMonth>();
    public double MaxMonthNet { get; set; } = 0;
}

public class InsightMonth
{
    public DateTime Month { get; set; }
    public double Spent { get; set; }
    public double Net { get; set; }
    public double Income { get; set; }
    public IEnumerable<InsightMonthGroup> GroupStats { get; set; } = Array.Empty<InsightMonthGroup>();
}

public class InsightMonthGroup
{
    public DateTime Month { get; set; }
    public Domain.Group Group { get; set; }
    public int TransactionCount { get; set; }
    public double Delta { get; set; }
}