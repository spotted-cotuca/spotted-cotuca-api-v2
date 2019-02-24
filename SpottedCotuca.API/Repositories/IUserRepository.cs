using SpottedCotuca.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Repositories
{
    interface IUserRepository
    {
        Task<User> Read(string username);
        Task Create(User user);
        //Task Update(User user);
        Task Delete(string username);
    }
}
