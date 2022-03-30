namespace Presentation.Store.Insights;

public record InsightsState
{
    public IEnumerable<InsightMonth> MonthInsights { get; set; }
    public double MaxMonthNet { get; set; }
}

public class InsightMonth
{
    public DateTime Month { get; set; }
    public double Spent { get; set; }
    public double Net { get; set; }
    public double Income { get; set; }
    public IEnumerable<InsightMonthGroup> GroupStats { get; set; }
}

public class InsightMonthGroup
{
    public string GroupName { get; set; }
    public int TransactionCount { get; set; }
    public double Delta { get; set; }
}