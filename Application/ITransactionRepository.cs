using Reconciler.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reconciler.Application
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> ReadTransactions();
        Task<Transaction> ReadTransaction(Guid id);
        Task Create(Transaction transaction);
        Task Update(Transaction transaction);
    }
}
