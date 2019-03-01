using SpottedCotuca.Application.Entities.Models;
using System.Threading.Tasks;
namespace SpottedCotuca.Application.Services
{
    public interface ISpotService
    {
        Task<Spot> ReadSpot(long id);
        Task<PagingSpots> ReadPagingSpots(Status status,int offset, int limit);
        Task CreateSpot(string message);
        Task UpdateSpot(long id, Status status);
        Task DeleteSpot(long id);
    }
}
