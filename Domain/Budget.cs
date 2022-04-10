namespace Domain;

public record Budget : IEntity
{
    public Guid Id { get; set; }
    public Group? Group { get; set; }
    public string Description { get; set; }
    public IEnumerable<BudgetInterval> Intervals { get; set; } = Array.Empty<BudgetInterval>();
}

public class BudgetInterval
{
    public DateTime From { get; set; }
    public TimeSpan? Interval { get; set; }
    public double Delta { get; set; }
}