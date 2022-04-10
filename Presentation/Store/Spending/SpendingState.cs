namespace Presentation.Store.Spending;

public record SpendingState
{
    public IEnumerable<SpendingMonth> SpendingMonths { get; set; } = Array.Empty<SpendingMonth>();
    public double MaxMonthNet { get; set; } = 0;
}

public class SpendingMonth
{
    public DateTime Month { get; set; }
    public double Spent { get; set; }
    public double Net { get; set; }
    public double Income { get; set; }
    public IEnumerable<SpendingMonthGroup> GroupStats { get; set; } = Array.Empty<SpendingMonthGroup>();
}

public class SpendingMonthGroup
{
    public DateTime Month { get; set; }
    public Domain.Group Group { get; set; }
    public int TransactionCount { get; set; }
    public double Delta { get; set; }
}