using Domain;

namespace Application;

public interface IRepo<TEntity> where TEntity : IEntity
{
    Task<IEnumerable<TEntity>> Read();
    Task<TEntity?> Read(Guid id);
    Task Create(TEntity entity);
    Task Create(params TEntity[] entities);
    Task Create(IEnumerable<TEntity> entities);
    Task Update(TEntity entity);
    Task Remove(params TEntity[] entities);
    Task Remove(IEnumerable<TEntity> entities);
}