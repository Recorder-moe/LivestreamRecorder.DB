#if COSMOSDB
namespace LivestreamRecorder.DB.CosmosDB;

// ReSharper disable once InconsistentNaming
public class UnitOfWork_Public : UnitOfWork
{
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public UnitOfWork_Public(PublicContext publicContext)
        : base(publicContext)
    {
    }
}
#endif
