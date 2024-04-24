using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace LivestreamRecorder.DB.Models;
#pragma warning disable CS8618 // 退出建構函式時，不可為 Null 的欄位必須包含非 Null 值。請考慮宣告為可為 Null。

public class Channel : Entity
{
#if COSMOSDB
    public Channel()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        Videos = new HashSet<Video>();
#pragma warning restore CS0618 // 類型或成員已經過時
    }
#endif

#if COUCHDB
    public override string Id
    {
        get => $"{Source}:{id}";
        set
        {
            Source = value.Split(':').First();
            id = value.Split(':').Last();
        }
    }
#endif

    [JsonPropertyName(nameof(ChannelName))]
    public string ChannelName { get; set; }

    [Required]
    [JsonPropertyName(nameof(Source))]
    public string Source { get; set; }

    [JsonPropertyName(nameof(Monitoring))] public bool Monitoring { get; set; } = false;

    [JsonPropertyName(nameof(Avatar))] public string? Avatar { get; set; }

    [JsonPropertyName(nameof(Banner))] public string? Banner { get; set; }

    [JsonPropertyName(nameof(LatestVideoId))]
    public string? LatestVideoId { get; set; }

    [JsonPropertyName(nameof(Hide))] public bool? Hide { get; set; } = false;

    [JsonPropertyName(nameof(UseCookiesFile))]
    public bool? UseCookiesFile { get; set; } = false;

    [JsonPropertyName(nameof(SkipNotLiveStream))]
    public bool? SkipNotLiveStream { get; set; } = true;

    [JsonPropertyName(nameof(AutoUpdateInfo))]
    public bool? AutoUpdateInfo { get; set; } = true;

    [JsonPropertyName(nameof(Note))] public string? Note { get; set; }

#if COSMOSDB
    [Obsolete("Relationship mapping is only supported in CosmosDB. Please avoid using it.")]
    public ICollection<Video> Videos { get; set; }
#endif
}
