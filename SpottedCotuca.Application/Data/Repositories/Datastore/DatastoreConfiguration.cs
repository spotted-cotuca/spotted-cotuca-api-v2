using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.Aplication.Repositories.Datastore
{
    public sealed class DatastoreConfiguration
    {
        private static DatastoreConfiguration instance = null;
        private static readonly object padlock = new object();

        private DatastoreDb db;
        private static readonly string _projectId;

        public DatastoreConfiguration()
        {
            db = DatastoreDb.Create(_projectId);
        }

        public static DatastoreConfiguration Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new DatastoreConfiguration();

                    return instance;
                }
            }
        }

        public static DatastoreDb DB
        {
            get
            {
                return Instance.db;
            }
        }
    }
}
