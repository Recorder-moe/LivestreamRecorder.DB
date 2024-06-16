using LivestreamRecorder.DB.Interfaces;
#if COSMOSDB
using LivestreamRecorder.DB.CosmosDB;
#elif COUCHDB
using LivestreamRecorder.DB.CouchDB;
#endif

namespace LivestreamRecorder.DB.Models;

public class ChannelRepository(IUnitOfWork unitOfWork) :
#if COSMOSDB
    CosmosDbRepository<Channel>(unitOfWork),
#elif COUCHDB
    CouchDbRepository<Channel>(unitOfWork),
#endif
    IChannelRepository
{
    public async Task<Channel?> GetChannelByIdAndSourceAsync(string channelId, string source)
#if COUCHDB
        => await base.GetByIdAsync($"{source}:{channelId}");
#elif COSMOSDB
        => (await base.GetByPartitionKeyAsync(source)).SingleOrDefault(p => p.id == channelId);
#endif

    public Task<List<Channel>> GetChannelsBySourceAsync(string source)
    {
        return base.GetByPartitionKeyAsync(source);
    }

    public override string CollectionName => "Channels";
}
