using SpottedCotuca.Application.Entities.Models;
using System.Threading.Tasks;

namespace SpottedCotuca.Aplication.Repositories
{
    public interface RoleRepository
    {
        Task<Role> Read(string name);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        Task Delete(string name);
    }
}
