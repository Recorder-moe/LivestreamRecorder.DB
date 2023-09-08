#if COSMOSDB
using LivestreamRecorder.DB.CosmosDB;
using Microsoft.EntityFrameworkCore;
#elif COUCHDB
using LivestreamRecorder.DB.CouchDB;
#endif
using LivestreamRecorder.DB.Interfaces;

namespace LivestreamRecorder.DB.Models;

public class VideoRepository :
# if COSMOSDB
    CosmosDbRepository<Video>,
#elif COUCHDB
    CouchDbRepository<Video>,
#endif
    IVideoRepository
{
    public VideoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Video?> GetVideoByIdAndChannelIdAsync(string videoId, string channelId)
#if COUCHDB
        => await base.GetByIdAsync($"{channelId}:{videoId}");
#elif COSMOSDB
        => (await base.GetByPartitionKeyAsync(channelId))
                      .Where(p => p.id == videoId)
                      .SingleOrDefault();
#endif

    public Task<List<Video>> GetVideosByChannelAsync(string channelId) => base.GetByPartitionKeyAsync(channelId);

    public override string CollectionName { get; } = "Videos";
}
