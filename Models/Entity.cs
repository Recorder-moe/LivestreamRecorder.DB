#if COUCHDB
using CouchDB.Driver.Types;
#endif
using LivestreamRecorder.DB.Interfaces;
using System.Text.Json.Serialization;

namespace LivestreamRecorder.DB.Models;

public abstract class Entity :
#if COUCHDB
    CouchDocument,
#endif
    IEntity
{
    private string? _id;

    /// <summary>
    /// Entity identifier
    /// </summary>
    [JsonPropertyName("id")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
    public virtual string id
    {
        get
        {
            if (string.IsNullOrEmpty(_id))
            {
#if COUCHDB
                _id = base.Id.Split(':').Last();
#else
                _id = Guid.NewGuid().ToString().Replace("-", "");
#endif
            }
            return _id;
        }

        set => _id = value ?? _id;
    }

#if COUCHDB
    [JsonPropertyName("_id")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
    public override string Id
    {
        get => $"{id}:{id}";
        set => id = value?.Split(':').Last() ?? "";
    }
#endif
}