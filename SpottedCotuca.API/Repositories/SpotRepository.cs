using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.API.Models;
using SpottedCotuca.API.Utils;

namespace SpottedCotuca.API.Repositories
{
    public class SpotRepository : ISpotRepository
    {
        private readonly string _projectId;
        private readonly DatastoreDb _db;

        public SpotRepository(string projectId)
        {
            _projectId = projectId;
            _db = DatastoreDb.Create(_projectId);
        }

        public void Create(Spot spot)
        {
            var entity = spot.ToEntity();
            entity.Key = _db.CreateKeyFactory("Spot").CreateIncompleteKey();
            var keys = _db.Insert(new[] { entity });
            spot.Id = keys.First().Path.First().Id;
        }

        public async Task CreateAsync(Spot spot)
        {
            var entity = spot.ToEntity();
            entity.Key = _db.CreateKeyFactory("Spot").CreateIncompleteKey();
            var keys = await _db.InsertAsync(new[] { entity });
            spot.Id = keys.First().Path.First().Id;
        }

        public void Delete(long id)
        {
            _db.Delete(id.ToKey());
        }

        public async Task DeleteAsync(long id)
        {
            await _db.DeleteAsync(id.ToKey());
        }

        public Spot Read(long id)
        {
            return _db.Lookup(id.ToKey())?.ToSpot();
        }

        public async Task<Spot> ReadAsync(long id)
        {
            var result = await _db.LookupAsync(id.ToKey());
            return result.ToSpot();
        }

        public PagingSpots Read(Status status, int offset, int limit)
        {
            var filter = new Filter();
            var query = new Query("Spot") { Filter = filter, Offset = offset, Limit = limit };

            var results = _db.RunQuery(query);

            return new PagingSpots(results.Entities.Select(entity => entity.ToSpot()).ToList(), offset, limit);
        }

        public async Task<PagingSpots> ReadAsync(Status status, int offset, int limit)
        {
            var filter = new Filter();
            var query = new Query("Spot") { Filter = filter, Offset = offset, Limit = limit };

            var results = await _db.RunQueryAsync(query);

            return new PagingSpots(results.Entities.Select(entity => entity.ToSpot()).ToList(), offset, limit);
        }

        public void Update(Spot spot)
        {
            _db.Update(spot.ToEntity());
        }

        public async Task UpdateAsync(Spot spot)
        {
            await _db.UpdateAsync(spot.ToEntity());
        }
    }
}
