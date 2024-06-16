#if COSMOSDB
namespace LivestreamRecorder.DB.CosmosDB;

// ReSharper disable once InconsistentNaming
public class UnitOfWork_Public(PublicContext publicContext) : UnitOfWork(publicContext);
#endif
