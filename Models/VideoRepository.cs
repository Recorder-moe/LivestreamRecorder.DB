using LivestreamRecorder.DB.Interfaces;
#if COSMOSDB
using LivestreamRecorder.DB.CosmosDB;
#elif COUCHDB
using LivestreamRecorder.DB.CouchDB;
#endif

namespace LivestreamRecorder.DB.Models;

public class VideoRepository(IUnitOfWork unitOfWork) :
# if COSMOSDB
    CosmosDbRepository<Video>(unitOfWork),
#elif COUCHDB
    CouchDbRepository<Video>(unitOfWork),
#endif
    IVideoRepository
{
    public async Task<Video?> GetVideoByIdAndChannelIdAsync(string videoId, string channelId)
#if COUCHDB
        => await base.GetByIdAsync($"{channelId}:{videoId}");
#elif COSMOSDB
        => (await base.GetByPartitionKeyAsync(channelId)).SingleOrDefault(p => p.id == videoId);
#endif

    public Task<List<Video>> GetVideosByChannelAsync(string channelId)
    {
        return base.GetByPartitionKeyAsync(channelId);
    }

    public override string CollectionName { get; } = "Videos";
}
