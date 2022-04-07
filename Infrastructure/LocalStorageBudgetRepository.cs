using Blazored.LocalStorage;
using Domain;

namespace Infrastructure;

public class LocalStorageBudgetRepository : LocalStorageRepo<Budget>
{
    public LocalStorageBudgetRepository(ILocalStorageService localStorage) 
        : base(localStorage, "budgets") { }
}