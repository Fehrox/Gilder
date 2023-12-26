using Application;
using Blazored.LocalStorage;
using Domain;

namespace Infrastructure;

public abstract class LocalStorageRepo<TEntity> : IRepo<TEntity> where TEntity : IEntity
{
    private readonly string _key;
    private readonly ILocalStorageService _localStorage;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    protected LocalStorageRepo(ILocalStorageService localStorage, string key)
    {
        _localStorage = localStorage;
        _key = key;
    }

    public async Task<IEnumerable<TEntity>> Read(params Guid[] ids)
    {
        var entities = new List<TEntity>();
        foreach (var id in ids) {
            var entity = await Read(id);
            if (entity is not null)
                entities.Add(entity);
        }

        return entities;
    }

    public async Task Create(TEntity entity)
        => await Create(new[] {entity});

    public Task Create(params TEntity[] entities) 
        => Create(entities.AsEnumerable());

    public async Task Create(IEnumerable<TEntity> entities)
    {
        await _semaphore.WaitAsync();

        var entityList = (await ReadEntitiesFree()).ToList();
        entityList.AddRange(entities);

        await _localStorage.SetItemAsync(_key, entityList);
        _semaphore.Release();
    }

    public async Task<TEntity?> Read(Guid id)
    {
        await _semaphore.WaitAsync();

        var entities = await Read();
        var foundEntity = entities.FirstOrDefault(t => t.Id == id);

        _semaphore.Release();

        return foundEntity;
    }

    public async Task<IEnumerable<TEntity>> Read()
    {
        await _semaphore.WaitAsync();

        var entities = await ReadEntitiesFree();

        _semaphore.Release();

        return entities;
    }

    public async Task Update(TEntity entity)
    {
        await _semaphore.WaitAsync();

        var entities = await ReadEntitiesFree();
        var updatedEntities = new List<TEntity>();  
        foreach (var existing in entities) {
            var entityToSave =
                existing.Id == entity.Id
                    ? entity
                    : existing;
            updatedEntities.Add(entityToSave);
        }

        await _localStorage.SetItemAsync(_key, updatedEntities);

        _semaphore.Release();
    }

    public async Task Delete(Guid id)
    {
        await _semaphore.WaitAsync();

        var currentGroups = (await ReadEntitiesFree()).ToList();
        var newGroups = new List<TEntity>();
        foreach (var entity in currentGroups)
            if (id != entity.Id)
                newGroups.Add(entity);
        
        await _localStorage.SetItemAsync(_key, newGroups);
        
        _semaphore.Release();
    }

    public async Task Delete(params Guid[] ids)
    {
        await _semaphore.WaitAsync();
        
        var currentGroups = (await ReadEntitiesFree()).ToList();
        var newGroups = new List<TEntity>();
        foreach (var entity in currentGroups)
            if (ids.All(g => g != entity.Id))
                newGroups.Add(entity);
        
        await _localStorage.SetItemAsync(_key, newGroups);
        
        _semaphore.Release();
    }

    public Task Delete(params TEntity[] entities) 
        => Delete(entities.AsEnumerable());

    public async Task Delete(IEnumerable<TEntity> groups)
    {
        var groupIds = groups.Select(g => g.Id);
        await Delete(groupIds.ToArray());
    }

    private async Task<IEnumerable<TEntity>> ReadEntitiesFree()
    {
        if (!await _localStorage.ContainKeyAsync(_key))
            return new List<TEntity>().AsEnumerable();

        var entities = await _localStorage
            .GetItemAsync<IEnumerable<TEntity>>(_key);

        return entities;
    }
}