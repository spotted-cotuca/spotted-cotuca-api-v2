using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Aplication.Repositories.Datastore
{
    public class DatastoreUserRepository : IUserRepository
    {
        private DatastoreDb _db;
        private DatastoreProvider _provider;

        public DatastoreUserRepository(DatastoreProvider datastoreProvider)
        {
            _provider = datastoreProvider;
            _db = _provider.Db();
        }

        public async Task Create(User user)
        {
            var entity = user.ToEntity();
            entity.Key = _db.CreateKeyFactory("User").CreateIncompleteKey();
            var keys = await _db.InsertAsync(new[] { entity });
            user.Id = keys.First().Path.First().Id;
        }

        public async Task Delete(string username)
        {
            User targetUser = await this.Read(username);
            await _db.DeleteAsync(targetUser?.Id.ToUserKey());
        }

        public async Task<User> Read(string username)
        {
            var query = new Query("User")
            {
                Filter = Filter.Equal("username", username)
            };

            var results = await _db.RunQueryAsync(query);
            return results.Entities.First()?.ToUser();
        }

        /*
        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
        */
    }
}
