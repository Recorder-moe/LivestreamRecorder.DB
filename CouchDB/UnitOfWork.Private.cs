#if COUCHDB
namespace LivestreamRecorder.DB.CouchDB;

// ReSharper disable once InconsistentNaming
public class UnitOfWork_Private(CouchDBContext context) : UnitOfWork(context);
#endif
