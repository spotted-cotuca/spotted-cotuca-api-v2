using System;
using System.Threading.Tasks;
using SpottedCotuca.API.Models;
using SpottedCotuca.API.Repositories;

namespace SpottedCotuca.API.Services
{
    public class SpotService : ISpotService
    {
        private readonly ISpotRepository _repository;

        public SpotService(ISpotRepository repository)
        {
            _repository = repository;
        }

        public Task<Spot> ReadSpot(long id)
        {
            var spot = _repository.ReadAsync(id);
            return spot;
        }

        public Task<PagingSpots> ReadPagingSpots(Status status, int offset, int limit)
        {
            var pagingSpots = _repository.ReadAsync(status, offset, limit);
            return pagingSpots;
        }

        public async Task CreateSpot(string message)
        {
            var spot = new Spot
            {
                Message = message,
                Status = Status.PENDING,
                PostDate = DateTime.UtcNow
            };

            await _repository.CreateAsync(spot);
        }

        public async Task UpdateSpot(long id, Status status)
        {
            var spot = await _repository.ReadAsync(id);
            spot.Status = status;

            // TODO: Facebook and Twitter interactions with UnitOfWork

            await _repository.UpdateAsync(spot);
        }

        public async Task DeleteSpot(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
