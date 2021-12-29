using Application;
using Blazored.LocalStorage;
using Domain;

namespace Gilder.Infrastructure
{
    public class LocalStorageTransactionRepository : ITransactionRepository
    {
        private const string KEY = "transactions";
        private readonly ILocalStorageService _localStorage;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public LocalStorageTransactionRepository(ILocalStorageService localStorage) => 
            _localStorage = localStorage;

        public async Task Create(Transaction transaction) => 
            await Create(new[] {transaction});

        public async Task Create(IEnumerable<Transaction> transaction)
        {
            await _semaphore.WaitAsync();

            var transactions = (await ReadTransactionsFree()).ToList();
            transactions.AddRange(transaction);

            await _localStorage.SetItemAsync(KEY, transactions);
            _semaphore.Release();
        }

        public async Task<Transaction> ReadTransaction(Guid id)
        {
            await _semaphore.WaitAsync();

            var transactions = await ReadTransactions();
            var foundTransaction = transactions.FirstOrDefault(t => t.Id == id);

            _semaphore.Release();

            return foundTransaction;
        }

        public async Task<IEnumerable<Transaction>> ReadTransactions()
        {
            await _semaphore.WaitAsync();

            var transactions = await ReadTransactionsFree();

            _semaphore.Release();

            return transactions;
        }

        public async Task Update(Transaction transaction)
        {
            await _semaphore.WaitAsync();

            var transactions = await ReadTransactionsFree();
            var updatedTransactions = new List<Transaction>();
            foreach (var existing in transactions) {
                var savedTranscation = 
                    existing.Id == transaction.Id 
                        ? transaction 
                        : existing;
                updatedTransactions.Add(savedTranscation);
            }

            await _localStorage.SetItemAsync(KEY, updatedTransactions);

            _semaphore.Release();
        }

        private async Task<IEnumerable<Transaction>> ReadTransactionsFree()
        {
            if (await _localStorage.ContainKeyAsync(KEY))
            {
                var transactions = await _localStorage
                    .GetItemAsync<IEnumerable<Transaction>>(KEY);

                return transactions;
            }

            return new List<Transaction>().AsEnumerable();
        }
    }
}
