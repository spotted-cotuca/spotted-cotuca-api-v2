using Google.Cloud.Datastore.V1;
using Grpc.Core;

namespace SpottedCotuca.Application.Data.Repositories.Datastore
{
    public interface DatastoreProvider
    {
        DatastoreDb Db {
            get;
        }
    }
}
