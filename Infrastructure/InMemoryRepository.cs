using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;

namespace Gilder.Infrastructure
{
    public class InMemoryRepository : ITransactionRepository, IGroupRepository
    {

        readonly List<Transaction> _transactions = new List<Transaction>();
        readonly List<Group> _groups = new List<Group>
        {
            new Group { Id = Guid.NewGuid(), Name = "Food" },
            new Group { Id = Guid.NewGuid(), Name = "Travel" },
            new Group { Id = Guid.NewGuid(), Name = "Exercise" },
            new Group { Id = Guid.NewGuid(), Name = "Going Out" },
        };

        public Task Create(Transaction transaction)
        {
            _transactions.Add(transaction);
            return Task.CompletedTask;
        }

        public Task Update(Transaction transaction)
        {
            foreach (var current in _transactions) {
                if (current.Id == transaction.Id) {
                    current.Note = transaction.Note;
                    current.Group = transaction.Group;
                    current.Details = transaction.Details;
                    current.Charge = transaction.Charge;
                    current.Class = transaction.Class;
                    current.Date = transaction.Date;
                }
            }

            return Task.CompletedTask;
        }

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
