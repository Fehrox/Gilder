﻿using Application;
using Domain;

namespace Infrastructure
{
    public class InMemoryRepository : ITransactionRepository, IGroupRepository
    {

        readonly List<Transaction> _transactions = new();
        readonly List<Group> _groups = new()
        {
            new() { Id = Guid.NewGuid(), Name = "Food" },
            new() { Id = Guid.NewGuid(), Name = "Travel" },
            new() { Id = Guid.NewGuid(), Name = "Exercise" },
            new() { Id = Guid.NewGuid(), Name = "Going Out" },
        };

        public Task Create(Transaction transaction)
        {
            _transactions.Add(transaction);
            return Task.CompletedTask;
        }

        public async Task Create(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions) 
                await Create(transaction);
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

        public async Task Create(IEnumerable<Group> groups)
        {
            foreach (var group in groups) 
                await Create(group);
        }

        public async Task Remove(IEnumerable<Group> groups)
        {
            // _groups.RemoveAll(groups);
            // await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public Task Create(Group group)
        {
            _groups.Add(group);
            return Task.CompletedTask;
        }

        Task<IEnumerable<Group>> IGroupRepository.ReadGroups() => Task.FromResult(_groups.AsEnumerable());

        Task<Transaction> ITransactionRepository.ReadTransaction(Guid id)
        {
            var transaction = _transactions.Find(t => t.Id == id);
            return Task.FromResult(transaction);
        }

        Task<IEnumerable<Transaction>> ITransactionRepository.ReadTransactions() => 
            Task.FromResult(_transactions.AsEnumerable());
    }
}
