#if COUCHDB
using CouchDB.Driver;
using CouchDB.Driver.Indexes;
using LivestreamRecorder.DB.Models;

namespace LivestreamRecorder.DB.CouchDB;

// ReSharper disable once InconsistentNaming
public class UnitOfWork_Public : UnitOfWork
{
    private readonly CouchDBContext _context;

    public UnitOfWork_Public(CouchDBContext context)
        : base(context)
    {
        _context = context;
        PrepareIndexesAsync().ConfigureAwait(false).GetAwaiter();
    }

    private async Task PrepareIndexesAsync()
    {
        var tasks = new List<Task>();
        await prepareVideoIndexAsync();
        await prepareChannelIndexAsync();
        await Task.WhenAll(tasks);
        return;

        async Task prepareVideoIndexAsync()
        {
            ICouchDatabase<Video> database = _context.Client.GetDatabase<Video>();
            var existIndexes = (await database.GetIndexesAsync()).ToHashSet();
            tasks.AddRange(_context.VideoIndexes
                                   .Where(index => existIndexes.All(p => p.Name != index.Key))
                                   .Select(index => database.CreateIndexAsync(index.Key,
                                                                              index.Value,
                                                                              new IndexOptions
                                                                              {
                                                                                  Partitioned = false,
                                                                              })));
        }

        async Task prepareChannelIndexAsync()
        {
            ICouchDatabase<Channel> database = _context.Client.GetDatabase<Channel>();
            var existIndexes = (await database.GetIndexesAsync()).ToHashSet();
            tasks.AddRange(_context.ChannelIndexes
                                   .Where(index => existIndexes.All(p => p.Name != index.Key))
                                   .Select(index => database.CreateIndexAsync(index.Key,
                                                                              index.Value,
                                                                              new IndexOptions
                                                                              {
                                                                                  Partitioned = false,
                                                                              })));
        }
    }
}
#endif
