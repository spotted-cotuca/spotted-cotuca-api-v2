using Google.Cloud.Datastore.V1;
using Grpc.Core;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedCotuca.Application.Tests.Respositories
{
    class TestDatastoreProvider : DatastoreProvider
    {
        private static readonly object padlock = new object();

        private readonly string _projectId;
        private DatastoreDb _db;

        public TestDatastoreProvider()
        {
            this._projectId = "newspottedctc-homolog";
        }

        public DatastoreDb Db
        {
            get
            {
                lock (padlock)
                {
                    if (_db == null)
                        _db = DatastoreDb.Create(
                            _projectId,
                            string.Empty,
                            new DatastoreClientImpl(
                                new Datastore.DatastoreClient(
                                    new Channel("localhost", 8081, ChannelCredentials.Insecure)
                                ),
                                new DatastoreSettings()
                            )
                        );

                    return _db;
                }
            }

            private set { }
        }
    }
}
