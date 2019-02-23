using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Repositories.Datastore
{
    public class DatastoreUserRepository : IUserRepository
    {
        public Task Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> Read(string username)
        {
            throw new NotImplementedException();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
