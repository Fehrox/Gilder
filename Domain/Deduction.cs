namespace Domain;

public class Deduction : IEntity
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public string Reason { get; set; }
}