using LivestreamRecorder.DB.Interfaces;
#if COSMOSDB
using LivestreamRecorder.DB.CosmosDB;
#elif COUCHDB
using LivestreamRecorder.DB.CouchDB;
#endif

namespace LivestreamRecorder.DB.Models;

public class UserRepository(IUnitOfWork unitOfWork) :
#if COSMOSDB
    CosmosDbRepository<User>(unitOfWork),
#elif COUCHDB
    CouchDbRepository<User>(unitOfWork),
#endif
    IUserRepository
{
    public override async Task<User?> GetByIdAsync(string id)
#if COUCHDB
        => await base.GetByIdAsync($"{id}:{id}");
#elif COSMOSDB
        => (await base.GetByPartitionKeyAsync(id)).SingleOrDefault(p => p.id == id);
#endif

    public override string CollectionName { get; } = "Users";
}
