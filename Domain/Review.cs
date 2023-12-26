namespace Domain;

public class Review : IEntity
{
    public Guid TransactionId { get; set; }
    public DateTime ReviewedAt { get; set; }
    public Guid Id { get; set; }
}