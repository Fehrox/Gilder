using System.Collections.Generic;
using System.Threading.Tasks;
using Reconciler.Domain;

namespace Reconciler.Application
{
    public interface ITransactionImporter
    {
        Task<IEnumerable<Transaction>> ImportTransactions();
        Task<Transaction> GetTransactionByHash(Hash transactionHash);
    }
}