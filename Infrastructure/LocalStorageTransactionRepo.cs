using Blazored.LocalStorage;
using Domain;

namespace Infrastructure
{
    public class LocalStorageTransactionRepo : LocalStorageRepo<Transaction>
    {
        public LocalStorageTransactionRepo(ILocalStorageService localStorage) 
            : base(localStorage, "transactions") { }
    }
}
