using SpottedCotuca.Application.Contracts.Responses;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.Application.Services.Utils
{
    public static class MapperExtensions
    {
        public static SpotGetResponse ToSpotGetResponse(this Spot spot)
        {
            return new SpotGetResponse
            {
                Id = spot.Id,
                Message = spot.Message,
                Status = spot.Status.ToString(),
                PostDate = spot.PostDate,
                FacebookId = spot.FacebookId,
                TwitterId = spot.TwitterId
            };
        }

        public static SpotsGetResponse ToSpotsGetResponse(this PagingSpots pagingSpots)
        {
            var spots = pagingSpots.Spots.ConvertAll(spot => spot.ToSpotGetResponse());

            var pagingResponse = new PagingResponse
            {
                Limit = pagingSpots.Limit,
                Offset = pagingSpots.Offset,
                Count = pagingSpots.Count
            };

            return new SpotsGetResponse
            {
                Spots = spots,
                Paging = pagingResponse
            };
        }

        public static SpotPostResponse ToSpotPostResponse(this Spot spot)
        {
            return new SpotPostResponse{ Id = spot.Id };
        }

        public static SpotPutResponse ToSpotPutResponse(this Spot spot)
        {
            return new SpotPutResponse
            {
                Id = spot.Id,
                Message = spot.Message,
                Status = spot.Status.ToString(),
                FacebookId = spot.FacebookId,
                TwitterId = spot.TwitterId
            };
        }
    }
}
