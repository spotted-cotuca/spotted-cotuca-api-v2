using SpottedCotuca.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Services
{
    public interface IUserService
    {
        Task<User> ReadUser(string username);
        Task Signup(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string username);
    }
}
