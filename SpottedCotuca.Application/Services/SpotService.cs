using System;
using System.Linq;
using System.Threading.Tasks;
using SpottedCotuca.Aplication.Repositories;
using SpottedCotuca.Application.Contracts;
using SpottedCotuca.Application.Contracts.Requests;
using SpottedCotuca.Application.Contracts.Responses;
using SpottedCotuca.Application.Services.Definitions;

namespace SpottedCotuca.Application.Services
{
    public class SpotService : BaseService
    {
        private readonly ISpotRepository _repository;

        public SpotService(ISpotRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<SpotGetResponse>> ReadSpot(long id)
        {
            if (!IsValidId(id))
                return Error<SpotGetResponse>(Errors.SpotIdIsInvalid);

            throw new NotImplementedException();

            //var spot = _repository.Read(id);
            //return spot;
        }

        public async Task<Result<SpotsGetResponse>> ReadSpots(SpotsGetRequest request)
        {
            var validate = await request.ValidateAsync();
            if (!validate.IsValid)
            {
                var error = validate.Errors.FirstOrDefault();
                return Error<SpotsGetResponse>(error.ToMetaError());
            }

            throw new NotImplementedException();

            //var pagingSpots = _repository.Read(status, offset, limit);
            //return pagingSpots;
        }

        public async Task<Result<SpotPostResponse>> CreateSpot(SpotPostRequest request)
        {
            var validate = await request.ValidateAsync();
            if (!validate.IsValid)
            {
                var error = validate.Errors.FirstOrDefault();
                return Error<SpotPostResponse>(error.ToMetaError());
            }

            throw new NotImplementedException();

            //var spot = new Spot
            //{
            //    Message = message,
            //    Status = Status.Pending,
            //    PostDate = DateTime.UtcNow
            //};

            //await _repository.Create(spot);
        }

        public async Task<Result<SpotPutResponse>> UpdateSpot(long id, SpotPutRequest request)
        {
            if (!IsValidId(id))
                return Error<SpotPutResponse>(Errors.SpotIdIsInvalid);

            var validate = await request.ValidateAsync();
            if (!validate.IsValid)
            {
                var error = validate.Errors.FirstOrDefault();
                return Error<SpotPutResponse>(error.ToMetaError());
            }

            throw new NotImplementedException();

            //var spot = await _repository.Read(id);
            //spot.Status = status;

            //// TODO: Facebook and Twitter interactions with UnitOfWork

            //await _repository.Update(spot);
        }

        public async Task<Result> DeleteSpot(long id)
        {
            if (!IsValidId(id))
                return Error<SpotPutResponse>(Errors.SpotIdIsInvalid);

            throw new NotImplementedException();

            //await _repository.Delete(id);
        }

        private bool IsValidId(long id)
        {
            if (id < 1000000000000000 || id > 9999999999999999)
                return false;

            return true;
        }
    }
}
