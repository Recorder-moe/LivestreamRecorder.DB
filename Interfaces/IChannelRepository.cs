using LivestreamRecorder.DB.Models;

namespace LivestreamRecorder.DB.Interfaces;

public interface IChannelRepository : IRepository<Channel>
{
    Task<Channel?> GetChannelByIdAndSourceAsync(string channelId, string source);
    Task<List<Channel>> GetChannelsBySourceAsync(string source);
}
