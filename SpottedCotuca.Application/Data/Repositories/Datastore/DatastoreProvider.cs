using Google.Api.Gax.Grpc;
using Google.Cloud.Datastore.V1;

namespace SpottedCotuca.Application.Data.Repositories.Datastore
{
    public class DatastoreProvider
    {
        private static readonly object padlock = new object();

        private readonly DatastoreOptions _options;
        private DatastoreClient _client;
        private DatastoreDb _db;

        public DatastoreProvider(DatastoreOptions options)
        {
            _options = options;

            if (string.IsNullOrEmpty(_options.Host))
                _client = DatastoreClient.Create(new ServiceEndpoint(_options.Host, _options.Port));
        }

        public DatastoreDb Db {
            get {
                lock (padlock)
                {
                    if (_db == null)
                    {
                        if (_client != null)
                            _db = DatastoreDb.Create(_options.ProjectId, _options.NamespaceId, _client);

                        _db = DatastoreDb.Create(_options.ProjectId, _options.NamespaceId);
                    }

                    return _db;
                }
            }

            private set { }
        }
    }

    public class DatastoreOptions
    {
        public string ProjectId;
        public string NamespaceId;
        public string Host;
        public int Port;
    }
}
