#if COUCHDB
using CouchDB.Driver;
using CouchDB.Driver.Indexes;
using CouchDB.Driver.Options;
using LivestreamRecorder.DB.Models;

namespace LivestreamRecorder.DB.CouchDB;

// ReSharper disable once InconsistentNaming
// ReSharper disable once ClassNeverInstantiated.Global
public class CouchDBContext : CouchContext
{
    public CouchDBContext()
    {
    }

    public CouchDBContext(CouchOptions options) : base(options)
    {
    }

    internal readonly Dictionary<string, Action<IIndexBuilder<Video>>> VideoIndexes = new()
    {
        #region Used by frontend

        {
            "_id",
            (builder) => builder.IndexBy(p => p.Id)
        },
        {
            "TimestampsPublishedAt, _id",
            (builder) => builder.IndexByDescending(p => p.Timestamps.PublishedAt)
                                .ThenByDescending(p => p.Id)
        },
        {
            "TimestampsPublishedAt, ChannelId",
            (builder) => builder.IndexByDescending(p => p.Timestamps.PublishedAt)
                                .ThenByDescending(p => p.ChannelId)
        },
        {
            "ArchivedTime, _id, Status",
            (builder) => builder.IndexByDescending(p => p.ArchivedTime)
                                .ThenByDescending(p => p.Id)
                                .ThenByDescending(p => p.Status)
        },
        {
            "ArchivedTime, _id, Status, SourceStatus",
            (builder) => builder.IndexByDescending(p => p.ArchivedTime)
                                .ThenByDescending(p => p.Id)
                                .ThenByDescending(p => p.Status)
                                .ThenByDescending(p => p.SourceStatus)
        },

        #endregion

        #region Used by service

        {
            "Status",
            (builder) => builder.IndexByDescending(p => p.Status)
        },
        {
            "Source",
            (builder) => builder.IndexByDescending(p => p.Source)
        }

        #endregion
    };

    internal readonly Dictionary<string, Action<IIndexBuilder<Channel>>> ChannelIndexes = new()
    {
        #region Used by service

        {
            "_id",
            (builder) => builder.IndexBy(p => p.Id)
        },
        {
            "Source",
            (builder) => builder.IndexByDescending(p => p.Source)
        }

        #endregion
    };

    protected override void OnDatabaseCreating(CouchDatabaseBuilder databaseBuilder)
    {
        #region Videos

        databaseBuilder.Document<Video>()
                       .ToDatabase("videos");

        databaseBuilder.Document<Video>()
                       .IsPartitioned();

        foreach (KeyValuePair<string, Action<IIndexBuilder<Video>>> index in VideoIndexes)
        {
            databaseBuilder.Document<Video>()
                           .HasIndex(index.Key,
                               index.Value,
                               new IndexOptions
                                   { Partitioned = false, });
        }

        #endregion

        #region Channels

        databaseBuilder.Document<Channel>()
                       .ToDatabase("channels");

        databaseBuilder.Document<Channel>()
                       .IsPartitioned();

        foreach (KeyValuePair<string, Action<IIndexBuilder<Channel>>> index in ChannelIndexes)
        {
            databaseBuilder.Document<Channel>()
                           .HasIndex(index.Key,
                               index.Value,
                               new IndexOptions
                                   { Partitioned = false, });
        }

        #endregion

        #region Users

        databaseBuilder.Document<User>()
                       .ToDatabase("users");

        databaseBuilder.Document<User>()
                       .IsPartitioned();

        #endregion
    }
}
#endif
