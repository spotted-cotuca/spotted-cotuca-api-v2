using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Aplication.Repositories.Datastore
{
    public class DatastoreUserRepository : UserRepository
    {
        private DatastoreDb _db;
        private DatastoreProvider _provider;

        public DatastoreUserRepository(DatastoreProvider datastoreProvider)
        {
            _provider = datastoreProvider;
            _db = _provider.Db;
        }

        public async Task<User> Create(User user)
        {
            using (DatastoreTransaction transaction = await _db.BeginTransactionAsync())
            {
                var entity = user.ToEntity();
                entity.Key = _db.CreateKeyFactory("User").CreateIncompleteKey();
                transaction.Insert(entity);

                await transaction.CommitAsync();
                user.Id = entity.Key.ToId();
            }

            return user;
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

        public async Task<User> Update(User user)
        {
            using (DatastoreTransaction transaction = await _db.BeginTransactionAsync())
            {
                var entity = user.ToEntity();
                transaction.Update(entity);
                await transaction.CommitAsync();
            }

            return user;
        }
    }
}
