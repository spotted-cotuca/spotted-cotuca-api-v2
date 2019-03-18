using SpottedCotuca.Application.Entities.Models;
using System.Threading.Tasks;

namespace SpottedCotuca.Aplication.Repositories
{
    public interface SpotRepository
    {
        Task<Spot> Read(long id);
        Task<PagingSpots> Read(Status status, int offset, int limit);
        Task<Spot> Create(Spot spot);
        Task<Spot> Update(Spot spot);
        Task Delete(long id);
    }
}
