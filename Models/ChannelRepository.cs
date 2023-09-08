#if COSMOSDB
using LivestreamRecorder.DB.CosmosDB;
using Microsoft.EntityFrameworkCore;
#elif COUCHDB
using LivestreamRecorder.DB.CouchDB;
#endif
using LivestreamRecorder.DB.Interfaces;

namespace LivestreamRecorder.DB.Models;

public class ChannelRepository :
#if COSMOSDB
    CosmosDbRepository<Channel>,
#elif COUCHDB
    CouchDbRepository<Channel>,
#endif
    IChannelRepository
{
    public ChannelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Channel?> GetChannelByIdAndSourceAsync(string channelId, string source)
#if COUCHDB
        => await base.GetByIdAsync($"{source}:{channelId}");
#elif COSMOSDB
        => (await base.GetByPartitionKeyAsync(source))
                      .Where(p => p.id == channelId)
                      .SingleOrDefault();
#endif

    public Task<List<Channel>> GetChannelsBySourceAsync(string source) => base.GetByPartitionKeyAsync(source);

    public override string CollectionName { get; } = "Channels";
}
