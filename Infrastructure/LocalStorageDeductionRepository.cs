using Blazored.LocalStorage;
using Domain;

namespace Infrastructure;

public class LocalStorageDeductionRepository : LocalStorageRepo<Deduction>
{
    public LocalStorageDeductionRepository(ILocalStorageService localStorage) 
        : base(localStorage, "deductions") { }
}