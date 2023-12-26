using Domain;

namespace Application;

public interface IRepo<TEntity> where TEntity : IEntity
{
    Task<IEnumerable<TEntity>> Read();
    Task<TEntity?> Read(Guid id);
    Task<IEnumerable<TEntity>> Read(params Guid[] ids);
    Task Create(TEntity entity);
    Task Create(params TEntity[] entities);
    Task Create(IEnumerable<TEntity> entities);
    Task Update(TEntity entity);
    Task Delete(Guid id);
    Task Delete(params Guid[] ids);
    Task Delete(params TEntity[] entities);
    Task Delete(IEnumerable<TEntity> entities);
}