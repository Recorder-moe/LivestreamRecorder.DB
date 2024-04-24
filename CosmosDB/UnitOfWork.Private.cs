#if COSMOSDB
namespace LivestreamRecorder.DB.CosmosDB;

// ReSharper disable once InconsistentNaming
public class UnitOfWork_Private : UnitOfWork
{
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public UnitOfWork_Private(PrivateContext privateContext)
        : base(privateContext)
    {
    }
}
#endif
