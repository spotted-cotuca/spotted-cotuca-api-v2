using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.API.Models;
using SpottedCotuca.API.Utils;

namespace SpottedCotuca.API.Repositories
{
    public class DatastoreSpotRepository : ISpotRepository
    {
        private readonly string _projectId;
        private readonly DatastoreDb _db;

        public DatastoreSpotRepository(string projectId)
        {
            _projectId = projectId;
            _db = DatastoreDb.Create(_projectId);
        }

        public async Task<Spot> Read(long id)
        {
            var result = await _db.LookupAsync(id.ToKey());
            return result.ToSpot();
        }

        public async Task<PagingSpots> Read(Status status, int offset, int limit)
        {
            var filter = new Filter();
            var query = new Query("Spot") { Filter = filter, Offset = offset, Limit = limit };

            var results = await _db.RunQueryAsync(query);

            return new PagingSpots(results.Entities.Select(entity => entity.ToSpot()).ToList(), offset, limit);
        }

        public async Task Create(Spot spot)
        {
            var entity = spot.ToEntity();
            entity.Key = _db.CreateKeyFactory("Spot").CreateIncompleteKey();
            var keys = await _db.InsertAsync(new[] { entity });
            spot.Id = keys.First().Path.First().Id;
        }

        public async Task Update(Spot spot)
        {
            await _db.UpdateAsync(spot.ToEntity());
        }

        public async Task Delete(long id)
        {
            await _db.DeleteAsync(id.ToKey());
        }
    }
}
