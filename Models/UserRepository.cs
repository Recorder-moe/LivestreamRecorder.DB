#if COSMOSDB
using LivestreamRecorder.DB.CosmosDB;
#elif COUCHDB
using LivestreamRecorder.DB.CouchDB;
#endif
using LivestreamRecorder.DB.Interfaces;

namespace LivestreamRecorder.DB.Models;

public class UserRepository :
#if COSMOSDB
    CosmosDbRepository<User>,
#elif COUCHDB
    CouchDbRepository<User>,
#endif
    IUserRepository
{
    public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<User?> GetByIdAsync(string id)
#if COUCHDB
        => await base.GetByIdAsync($"{id}:{id}");
#elif COSMOSDB
        => (await base.GetByPartitionKeyAsync(id))
            .SingleOrDefault(p => p.id == id);
#endif

    public override string CollectionName { get; } = "Users";
}
