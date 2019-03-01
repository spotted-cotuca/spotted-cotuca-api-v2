using System.Threading.Tasks;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.Application.Services
{
    public interface IUserService
    {
        Task<User> ReadUser(string username);
        Task Signup(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string username);
    }
}
