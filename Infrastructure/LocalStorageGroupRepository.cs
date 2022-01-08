using Application;
using Blazored.LocalStorage;
using Domain;

namespace Gilder.Infrastructure;

public class LocalStorageGroupRepository : IGroupRepository
{
    private const string KEY = "groups";
    private readonly ILocalStorageService _localStorage;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public LocalStorageGroupRepository(ILocalStorageService localStorage) => 
        _localStorage = localStorage;
    
    public async Task<IEnumerable<Group>> ReadGroups()
    {
        await _semaphore.WaitAsync();

        var transactions = await ReadGroupFree();

        _semaphore.Release();

        return transactions;
    }

    public async Task Create(IEnumerable<Group> groups)
    {
        await _semaphore.WaitAsync();

        var currentGroups = (await ReadGroupFree()).ToList();
        currentGroups.AddRange(groups);

        await _localStorage.SetItemAsync(KEY, currentGroups);
        _semaphore.Release();
    }

    public async Task Remove(IEnumerable<Group> groups)
    {
        await _semaphore.WaitAsync();

        var groupsList = groups.ToList();
        var currentGroups = (await ReadGroupFree()).ToList();
        var newGroups = new List<Group>();
        foreach (var @group in currentGroups)
            if (groupsList.All(g => g.Id != @group.Id))
                newGroups.Add(@group);

        // var newGroupsList = newGroups.Select(x => x.Name);
        // var strTest = String.Join(",", newGroupsList);
        // Console.WriteLine(strTest);
        //
        // var newGroupsLists = groups.Select(x => x.Id);
        // var strTests = String.Join(",", newGroupsLists);
        // Console.WriteLine(strTests);

        await _localStorage.SetItemAsync(KEY, newGroups);
        
        _semaphore.Release();
    }

    private async Task<IEnumerable<Group>> ReadGroupFree()
    {
        if (!await _localStorage.ContainKeyAsync(KEY))
            return new List<Group>().AsEnumerable();
        
        var transactions = await _localStorage
            .GetItemAsync<IEnumerable<Group>>(KEY);

        return transactions;

    }
}