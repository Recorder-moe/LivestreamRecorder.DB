#if COUCHDB
namespace LivestreamRecorder.DB.CouchDB
{
    // ReSharper disable once InconsistentNaming
    public class UnitOfWork_Private : UnitOfWork
    {
        public UnitOfWork_Private(CouchDBContext context)
            : base(context)
        {
        }
    }
}
#endif
