using System.Threading.Tasks;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.Aplication.Repositories
{
    interface UserRepository
    {
        Task<User> Read(string username);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task Delete(string username);
    }
}
