using System.Threading.Tasks;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.Aplication.Repositories
{
    interface IUserRepository
    {
        Task<User> Read(string username);
        Task Create(User user);
        //Task Update(User user);
        Task Delete(string username);
    }
}
