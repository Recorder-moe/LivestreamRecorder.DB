using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LivestreamRecorder.DB.Models;
#pragma warning disable CS8618 // 退出建構函式時，不可為 Null 的欄位必須包含非 Null 值。請考慮宣告為可為 Null。

public class User : Entity
{
    [Required]
    [JsonPropertyName(nameof(UserName))]
    public string UserName { get; set; }

    [Required]
    [JsonPropertyName(nameof(Email))]
    public string Email { get; set; }

    [JsonPropertyName(nameof(Picture))]
    public string? Picture { get; set; }

    [JsonPropertyName(nameof(RegistrationDate))]
    public DateTime RegistrationDate { get; set; }

    [JsonPropertyName(nameof(Note))]
    public string? Note { get; set; }

    [JsonPropertyName(nameof(GoogleUID))]
    public string? GoogleUID { get; set; }

    [JsonPropertyName(nameof(GithubUID))]
    public string? GithubUID { get; set; }

    [JsonPropertyName(nameof(MicrosoftUID))]
    public string? MicrosoftUID { get; set; }

    [JsonPropertyName(nameof(DiscordUID))]
    public string? DiscordUID { get; set; }

    [JsonPropertyName(nameof(IsAdmin))]
    public bool IsAdmin { get; set; } = false;
}

