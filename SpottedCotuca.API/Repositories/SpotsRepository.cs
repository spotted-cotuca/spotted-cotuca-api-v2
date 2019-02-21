using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Repositories
{
    public class SpotsRepository : ISpotsRepository
    {
        private readonly string _projectId;
        private readonly DatastoreDb _db;

        public SpotsRepository(string projectId)
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

        public void Delete(long id)
        {
            _db.Delete(id.ToKey());
        }

        public Spot Read(long id)
        {
            return _db.Lookup(id.ToKey())?.ToSpot();
        }

        public List<Spot> Read(Status status, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public void Update(Spot spot)
        {
            _db.Update(spot.ToEntity());
        }
    }
}
