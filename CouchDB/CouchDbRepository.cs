#if COUCHDB
using CouchDB.Driver;
using CouchDB.Driver.Extensions;
using CouchDB.Driver.Types;
using CouchDB.Driver.Views;
using Flurl.Http;
using LivestreamRecorder.DB.Exceptions;
using LivestreamRecorder.DB.Interfaces;
using LivestreamRecorder.DB.Models;
using System.Linq.Expressions;

namespace LivestreamRecorder.DB.CouchDB;

public abstract class CouchDbRepository<T> : IRepository<T> where T : Entity
{
    public abstract string CollectionName { get; }
    private readonly CouchContext _context;

    public CouchDbRepository(IUnitOfWork unitOfWork)
    {
        UnitOfWork u = (UnitOfWork)unitOfWork;
        _context = u.Context;
    }

    private ICouchDatabase<T>? _database;

    private ICouchDatabase<T> Database
    {
        get
        {
            _database ??= _context.Client.GetDatabase<T>();
            return _database;
        }
    }

    public virtual IQueryable<T> All()
        => Database.AsQueryable();

    public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        => Database.Where(predicate);

    // ReSharper disable once InconsistentNaming
#pragma warning disable IDE1006
    public virtual Task<T?> GetByIdAsync(string Id)
#pragma warning restore IDE1006
        => Database.FindAsync(Id);

    public virtual async Task<List<T>> GetByPartitionKeyAsync(string partitionKey)
    {
        IFlurlRequest? request = Database.NewRequest().AppendPathSegments("_partition", partitionKey, "_all_docs");

        CouchViewList<string, RevisionInfo, T>? response = await request.GetJsonAsync<CouchViewList<string, RevisionInfo, T>>();
        var keys = response.Rows.Select(p => p.Id).ToList();

        return await Database.FindManyAsync(keys.AsReadOnly());
    }

    // ReSharper disable once InconsistentNaming
#pragma warning disable IDE1006
    public virtual bool Exists(string Id)
#pragma warning restore IDE1006
        => Database.Any(p => p.Id == Id);

    public virtual Task<T> AddOrUpdateAsync(T entity)
        => Database.AddOrUpdateAsync(entity);

    public virtual async Task DeleteAsync(T entity)
    {
        T? entityToDelete = await GetByIdAsync(entity.Id);
        if (null == entityToDelete) throw new EntityNotFoundException($"Entity with id: {entity.Id} was not found.");

        await Database.RemoveAsync(entityToDelete);
    }

    // Issue: couchdb-net doesn't support Linq Count() on IQueryable<T>.
    // https://github.com/matteobortolazzo/couchdb-net/issues/124
    public virtual async Task<int> CountAsync()
        => (await Database.ToCouchListAsync()).Count;

    public Task<T?> ReloadEntityFromDBAsync(T entity) => GetByIdAsync(entity.Id);
}
#endif
