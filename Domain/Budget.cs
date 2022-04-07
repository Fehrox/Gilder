namespace Domain;

public class Budget : IEntity
{
    public Guid Id { get; set; }
    public Group? Group { get; set; }
    public double Delta { get; set; }
    public DateTime From { get; set; }
}