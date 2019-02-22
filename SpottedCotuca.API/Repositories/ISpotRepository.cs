using SpottedCotuca.API.Models;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Repositories
{
    public interface ISpotRepository
    {
        Spot Read(long id);
        Task<Spot> ReadAsync(long id);
        PagingSpots Read(Status status, int offset, int limit);
        Task<PagingSpots> ReadAsync(Status status, int offset, int limit);
        void Create(Spot spot);
        Task CreateAsync(Spot spot);
        void Update(Spot spot);
        Task UpdateAsync(Spot spot);
        void Delete(long id);
        Task DeleteAsync(long id);
    }
}
