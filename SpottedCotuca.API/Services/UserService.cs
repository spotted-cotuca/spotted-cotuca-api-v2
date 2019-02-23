using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Services
{
    public class UserService : IUserService
    {
        public Task DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> ReadUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task Signup(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
