namespace Domain;

public class Import : IEntity
{
    public Guid Id { get; set; }

    public DateTime Created { get; }
    public IEnumerable<Guid> TransactionIds { get; set; }
    
    public Import(DateTime created, IEnumerable<Guid> transactionIds)
    {
        Id = Guid.NewGuid();
        Created = created;
        TransactionIds = transactionIds;
    }
}