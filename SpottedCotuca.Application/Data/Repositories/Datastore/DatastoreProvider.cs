using Google.Cloud.Datastore.V1;

namespace SpottedCotuca.Application.Data.Repositories.Datastore
{
    public class DatastoreProvider
    {
        private static readonly object padlock = new object();

        private readonly string _projectId;
        private DatastoreDb _db;

        public DatastoreProvider(string projectId)
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
