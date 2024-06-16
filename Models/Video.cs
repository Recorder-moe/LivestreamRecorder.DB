using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LivestreamRecorder.DB.Enums;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace LivestreamRecorder.DB.Models;
#pragma warning disable CS8618 // 退出建構函式時，不可為 Null 的欄位必須包含非 Null 值。請考慮宣告為可為 Null。

public class Video : Entity
{
#if COUCHDB
    public override string Id
    {
        get => $"{ChannelId}:{id}";
        set
        {
            ChannelId = value.Split(':').First();
            id = value.Split(':').Last();
        }
    }
#endif

    [Required]
    [JsonPropertyName(nameof(ChannelId))]
    public string ChannelId { get; set; }

    [Required]
    [JsonPropertyName(nameof(Source))]
    public string Source { get; set; }

    [JsonPropertyName(nameof(Status))] public VideoStatus Status { get; set; }

    [JsonPropertyName(nameof(IsLiveStream))]
    public bool? IsLiveStream { get; set; }

    [JsonPropertyName(nameof(Title))] public string Title { get; set; }

    [JsonPropertyName(nameof(Description))]
    public string? Description { get; set; }

    [JsonPropertyName(nameof(Timestamps))] public Timestamps Timestamps { get; set; }

    // My system upload timestamp
    [JsonPropertyName(nameof(ArchivedTime))]
    public DateTime? ArchivedTime { get; set; }

    [JsonPropertyName(nameof(Thumbnail))] public string? Thumbnail { get; set; }

    [JsonPropertyName(nameof(Filename))] public string? Filename { get; set; }

    [JsonPropertyName(nameof(Size))] public long? Size { get; set; }

    [JsonPropertyName(nameof(SourceStatus))]
    public VideoStatus? SourceStatus { get; set; } = VideoStatus.Unknown;

    [JsonPropertyName(nameof(Note))] public string? Note { get; set; }

#if COSMOSDB
    [Obsolete("Relationship mapping is only supported in CosmosDB. Please avoid using it.")]
    public Channel? Channel { get; set; }
#endif
}
