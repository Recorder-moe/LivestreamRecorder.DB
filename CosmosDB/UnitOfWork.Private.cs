#if COSMOSDB
namespace LivestreamRecorder.DB.CosmosDB;

// ReSharper disable once InconsistentNaming
public class UnitOfWork_Private(PrivateContext privateContext) : UnitOfWork(privateContext);
#endif
