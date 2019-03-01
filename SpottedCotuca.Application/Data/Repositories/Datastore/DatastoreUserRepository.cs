using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Aplication.Repositories.Datastore
{
    public class DatastoreUserRepository : IUserRepository
    {
        private DatastoreDb DB { get => DatastoreConfiguration.DB; }

        public async Task Create(User user)
        {
            var entity = user.ToEntity();
            entity.Key = DB.CreateKeyFactory("User").CreateIncompleteKey();
            var keys = await DB.InsertAsync(new[] { entity });
            user.Id = keys.First().Path.First().Id;
        }

        public async Task Delete(string username)
        {
            User targetUser = await this.Read(username);
            await DB.DeleteAsync(targetUser?.Id.ToUserKey());
        }

        public async Task<User> Read(string username)
        {
            var query = new Query("User")
            {
                Filter = Filter.Equal("username", username)
            };

            var results = await DB.RunQueryAsync(query);
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
