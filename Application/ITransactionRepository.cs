using System.Collections.Generic;
using System.Threading.Tasks;
using Reconciler.Domain;

namespace Reconciler.Application
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions();
    }
}