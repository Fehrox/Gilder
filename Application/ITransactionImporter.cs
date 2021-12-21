using Domain;

namespace Application
{
    public interface ITransactionImporter
    {
        Task<IEnumerable<Transaction>> ImportTransactions();
        Task<Transaction> GetTransactionByHash(Hash transactionHash);
    }
}