using Blazored.LocalStorage;
using Domain;

namespace Infrastructure;

public class LocalStorageImportRepository : LocalStorageRepo<Import>
{
    public LocalStorageImportRepository(ILocalStorageService localStorage) 
        : base(localStorage, "imports") { }
}