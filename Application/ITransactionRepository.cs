using Domain;

namespace Application
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> ReadTransactions();
        Task<Transaction> ReadTransaction(Guid id);
        Task Create(Transaction transaction);
        Task Create(IEnumerable<Transaction> transaction);
        Task Update(Transaction transaction);
    }
}
