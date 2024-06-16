using System.Linq.Expressions;

namespace LivestreamRecorder.DB.Interfaces;

public interface IRepository<T> where T : IEntity
{
    string CollectionName { get; }

    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> AddOrUpdateAsync(T entity);
    IQueryable<T> All();
    Task<int> CountAsync();
    Task DeleteAsync(T entity);
    bool Exists(string id);

    /// <summary>
    ///     Get a single entity by id. Will return null if no entity is found.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(string id);

    Task<List<T>> GetByPartitionKeyAsync(string partitionKey);

    // ReSharper disable once InconsistentNaming
    Task<T?> ReloadEntityFromDBAsync(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
}
