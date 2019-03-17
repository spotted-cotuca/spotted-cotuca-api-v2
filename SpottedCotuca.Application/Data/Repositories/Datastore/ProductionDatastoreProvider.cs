using Google.Cloud.Datastore.V1;
using Grpc.Core;

namespace SpottedCotuca.Application.Data.Repositories.Datastore
{
    public class ProductionDatastoreProvider : DatastoreProvider
    {
        private static readonly object padlock = new object();

        private readonly string _projectId;
        private DatastoreDb _db;

        public ProductionDatastoreProvider(string projectId)
        {
            _projectId = projectId;
        }

        public DatastoreDb Db {
            get {
                lock (padlock)
                {
                    if (_db == null)
                        _db = DatastoreDb.Create(_projectId);

                    return _db;
                }
            }

            private set { }
        }
    }
}
