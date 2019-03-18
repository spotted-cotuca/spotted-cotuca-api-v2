using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Aplication.Repositories.Datastore
{
    public class DatastoreSpotRepository : SpotRepository
    {
        private DatastoreDb _db;
        private DatastoreProvider _provider;

        public DatastoreSpotRepository(DatastoreProvider datastoreProvider)
        {
            _provider = datastoreProvider;
            _db = _provider.Db;
        }

        public async Task<Spot> Read(long id)
        {
            var result = await _db.LookupAsync(id.ToSpotKey());
            return result.ToSpot();
        }

        public async Task<PagingSpots> Read(Status status, int offset, int limit)
        {
            var filter = new Filter();
            var query = new Query("Spot") { Filter = filter, Offset = offset, Limit = limit };

            var results = await _db.RunQueryAsync(query);

            return new PagingSpots(results.Entities.Select(entity => entity.ToSpot()).ToList(), offset, limit);
        }

        public async Task<Spot> Create(Spot spot)
        {
            using (DatastoreTransaction transaction = await _db.BeginTransactionAsync())
            {
                var entity = spot.ToEntity();
                entity.Key = _db.CreateKeyFactory("Spot").CreateIncompleteKey();
                transaction.Insert(entity);

                await transaction.CommitAsync();
                spot.Id = entity.Key.ToId();
            }

            return spot;
        }

        public async Task<Spot> Update(Spot spot)
        {
            CommitResponse resp;

            using (DatastoreTransaction transaction = await _db.BeginTransactionAsync())
            {
                var entity = spot.ToEntity();
                transaction.Update(entity);
                resp = await transaction.CommitAsync();
            }

            return spot;
        }

        public async Task Delete(long id)
        {
            await _db.DeleteAsync(id.ToSpotKey());
        }
    }
}
