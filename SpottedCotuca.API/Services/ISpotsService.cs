using System.Collections.Generic;
using System.Threading.Tasks;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Services
{
    public interface ISpotsService
    {
        Task<Spot> GetSpot(long id);
        Task<List<Spot>> GetSpots(Status status, int limit, int offset);
        Task AddSpot(string message);
        Task UpdateSpotStatus(long id, Status status);
        Task DeleteSpot(long id);
    }
}
