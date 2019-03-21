using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Aplication.Repositories.Datastore
{
    public class DatastoreRoleRepository : RoleRepository
    {
        private DatastoreDb _db;
        private DatastoreProvider _provider;

        public DatastoreRoleRepository(DatastoreProvider datastoreProvider)
        {
            _provider = datastoreProvider;
            _db = _provider.Db;
        }

        public async Task<Role> Read(string name)
        {
            var query = new Query("Role")
            {
                Filter = Filter.Equal("name", name)
            };

            var results = await _db.RunQueryAsync(query);
            return results.Entities.First()?.ToRole();
        }
        
        public async Task<Role> Create(Role role)
        {
            using (DatastoreTransaction transaction = await _db.BeginTransactionAsync())
            {
                var entity = role.ToEntity();
                entity.Key = _db.CreateKeyFactory("Spot").CreateIncompleteKey();
                transaction.Insert(entity);

                await transaction.CommitAsync();
                role.Id = entity.Key.ToId();
            }

            return role;
        }

        public async Task<Role> Update(Role role)
        {
            CommitResponse resp;

            using (DatastoreTransaction transaction = await _db.BeginTransactionAsync())
            {
                var entity = role.ToEntity();
                transaction.Update(entity);
                resp = await transaction.CommitAsync();
            }

            return role;
        }

        public async Task Delete(string name)
        {
            Role targetRole = await this.Read(name);
            await _db.DeleteAsync(targetRole?.Id.ToUserKey());
        }
    }
}
