using Reconciler.Application;
using Reconciler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reconciler.Infrastructure
{
    public class InMemoryTransactionRepository : ITransactionRepository, IGroupRepository
    {

        readonly List<Transaction> _transactions = new List<Transaction>();
        readonly List<Group> _groups = new List<Group>
        {
            new Group { Id = Guid.NewGuid(), Name = "Food" },
            new Group { Id = Guid.NewGuid(), Name = "Travel" },
            new Group { Id = Guid.NewGuid(), Name = "Exercise" },
            new Group { Id = Guid.NewGuid(), Name = "Going Out" },
        };

        Task<Group> IGroupRepository.ReadGroup(Guid id)
        {
            var group = _groups.Find(g => g.Id == id);
            return Task.FromResult(group);
        }

        Task<IEnumerable<Group>> IGroupRepository.ReadGroups()
        {
            return Task.FromResult(_groups.AsEnumerable());
        }

        Task<Transaction> ITransactionRepository.ReadTransaction(Guid id)
        {
            var transaction = _transactions.Find(t => t.Id == id);
            return Task.FromResult(transaction);
        }

        Task<IEnumerable<Transaction>> ITransactionRepository.ReadTransactions()
        {
            return Task.FromResult(_transactions.AsEnumerable());
        }
    }
}
