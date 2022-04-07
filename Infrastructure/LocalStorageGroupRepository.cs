using Blazored.LocalStorage;
using Domain;

namespace Infrastructure;

public class LocalStorageGroupRepository : LocalStorageRepo<Group>
{
    public LocalStorageGroupRepository(ILocalStorageService localStorage) 
        : base(localStorage, "groups") { }

}