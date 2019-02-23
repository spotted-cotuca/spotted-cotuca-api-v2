using SpottedCotuca.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Repositories
{
    interface IUserRepository
    {
        Task<User> Read(String username);
        Task Create(User user);
        Task Update(User user);
        Task Delete(String username);
    }
}
