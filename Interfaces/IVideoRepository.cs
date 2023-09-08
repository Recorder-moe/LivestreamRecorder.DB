using LivestreamRecorder.DB.Models;

namespace LivestreamRecorder.DB.Interfaces;

public interface IVideoRepository : IRepository<Video>
{
    Task<Video?> GetVideoByIdAndChannelIdAsync(string videoId, string channelId);
    Task<List<Video>> GetVideosByChannelAsync(string channelId);
}
