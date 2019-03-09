using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Aplication.Repositories.Datastore
{
    public class DatastoreSpotRepository : ISpotRepository
    {
        public DatastoreDb DB { get => DatastoreConfiguration.DB; }

        public async Task<Spot> Read(long id)
        {
            var result = await DB.LookupAsync(id.ToSpotKey());
            return result.ToSpot();
        }

        public async Task<PagingSpots> Read(Status status, int offset, int limit)
        {
            var filter = new Filter();
            var query = new Query("Spot") { Filter = filter, Offset = offset, Limit = limit };

            var results = await DB.RunQueryAsync(query);

            return new PagingSpots(results.Entities.Select(entity => entity.ToSpot()).ToList(), offset, limit);
        }

        public async Task<Spot> Create(Spot spot)
        {
            CommitResponse resp;

            using (DatastoreTransaction transaction = await DB.BeginTransactionAsync())
            {
                var entity = spot.ToEntity();
                transaction.Insert(entity);
                resp = await transaction.CommitAsync();
                spot.Id = entity.Key.ToId();
            }

            return spot;
        }

        public async Task<Spot> Update(Spot spot)
        {
            CommitResponse resp;

            using (DatastoreTransaction transaction = await DB.BeginTransactionAsync())
            {
                var entity = spot.ToEntity();
                transaction.Update(entity);
                resp = await transaction.CommitAsync();
            }

            return spot;
        }

        public async Task Delete(long id)
        {
            await DB.DeleteAsync(id.ToSpotKey());
        }
    }
}
