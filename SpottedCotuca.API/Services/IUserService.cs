using SpottedCotuca.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Services
{
    interface IUserService
    {
        Task<User> ReadUser(String username);
        Task Signup(User user);
        Task UpdateUser(User user);
        Task DeleteUser(String username);
    }
}
