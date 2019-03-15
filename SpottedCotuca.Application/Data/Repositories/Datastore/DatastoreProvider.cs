using Google.Api.Gax.Grpc;
using Google.Cloud.Datastore.V1;

namespace SpottedCotuca.Application.Data.Repositories.Datastore
{
    public class DatastoreProvider
    {
        private static readonly object padlock = new object();

        private readonly DatastoreOptions _options;
        private DatastoreDb _db;

        public DatastoreProvider(DatastoreOptions options)
        {
            _options = options;
        }

        public DatastoreDb Db()
        {
            lock (padlock)
            {
                if (_db == null)
                    _db = DatastoreDb.Create(_options.ProjectId, _options.NamespaceId, _options.Client);

                return _db;
            }
        }
    }

    public class DatastoreOptions
    {
        public string ProjectId;
        public string NamespaceId;
        public ServiceEndpoint Endpoint;
        public DatastoreClient Client 
        {
            get {
                if (Endpoint == null)
                    return null;

                if (Client == null)
                    Client = DatastoreClient.Create(Endpoint, null);

                return Client;
            }

            private set { }
        }
    }
}
