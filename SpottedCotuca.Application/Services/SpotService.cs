using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpottedCotuca.Aplication.Repositories;
using SpottedCotuca.Application.Contracts;
using SpottedCotuca.Application.Contracts.Requests;
using SpottedCotuca.Application.Contracts.Responses;
using SpottedCotuca.Application.Data.Clients;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Services.Definitions;
using SpottedCotuca.Application.Services.Utils;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Application.Services
{
    public class SpotService : BaseService
    {
        private readonly SpotRepository _repository;
        private readonly IFacebookClient _facebookClient;
        private readonly ITwitterClient _twitterClient;

        public SpotService(SpotRepository repository, IFacebookClient facebookClient, ITwitterClient twitterClient)
        {
            _repository = repository;
            _facebookClient = facebookClient;
            _twitterClient = twitterClient;
        }

        public async Task<Result<SpotGetResponse>> ReadSpot(long id)
        {
            if (!IsValidId(id))
                return Error<SpotGetResponse>(Errors.SpotIdIsInvalid);

            Spot spot;

            try { spot = await _repository.Read(id); }
            catch { return Error<SpotGetResponse>(Errors.SpotReadingFromDatabaseError); }

            if (spot == null)
                return Error<SpotGetResponse>(Errors.SpotNotFoundError);

            return Success(spot.ToSpotGetResponse());
        }

        public async Task<Result<SpotsGetResponse>> ReadSpots(SpotsGetRequest request)
        {
            var validate = await request.ValidateAsync();
            if (!validate.IsValid)
            {
                var error = validate.Errors.FirstOrDefault();
                return Error<SpotsGetResponse>(error.ToMetaError());
            }

            List<Spot> spots;

            try { spots = await _repository.Read(request.Status.ToStatusEnum(), request.Offset, request.Limit);  }
            catch { return Error<SpotsGetResponse>(Errors.SpotsReadingFromDatabaseError); }

            if (spots?.Count == 0)
                return Error<SpotsGetResponse>(Errors.SpotsNotFoundError);

            return Success(spots.ToSpotsGetResponse(request.Offset, request.Limit));
        }

        public async Task<Result<SpotPostResponse>> CreateSpot(SpotPostRequest request)
        {
            var validate = await request.ValidateAsync();
            if (!validate.IsValid)
            {
                var error = validate.Errors.FirstOrDefault();
                return Error<SpotPostResponse>(error.ToMetaError());
            }

            var spot = new Spot
            {
                Message = request.Message,
                Status = Status.Pending,
                PostDate = DateTime.UtcNow,
            };

            try { spot = await _repository.Create(spot); }
            catch { return Error<SpotPostResponse>(Errors.SpotCreatingOnDatabaseError); }

            return Success(spot.ToSpotPostResponse());
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

            Spot spot;

            try { spot = await _repository.Read(id); }
            catch { return Error<SpotPutResponse>(Errors.SpotReadingFromDatabaseError); }

            if (spot == null)
                return Error<SpotPutResponse>(Errors.SpotNotFoundError);

            long facebookId = 0;
            long twitterId = 0;

            try
            {
                facebookId = await _facebookClient.CreatePost(spot.Message);
                twitterId = await _twitterClient.PublishTweet(spot.Message);
            }
            catch (FacebookClientException)
            {
                if (twitterId != 0)
                    await _twitterClient.DestroyTweet(twitterId);

                return Error<SpotPutResponse>(Errors.SpotCreatingFacebookPostError);
            }
            catch (TwitterClientException)
            {
                if (facebookId != 0)
                    await _facebookClient.DeletePost(facebookId);

                return Error<SpotPutResponse>(Errors.SpotPublishingTweetError);
            };

            spot.Status = request.Status.ToStatusEnum();
            spot.FacebookId = facebookId;
            spot.TwitterId = twitterId;

            try {
                spot = await _repository.Update(spot);
            }
            catch
            {
                await _facebookClient.DeletePost(facebookId);
                await _twitterClient.DestroyTweet(twitterId);

                return Error<SpotPutResponse>(Errors.SpotUpdatingOnDatabaseError);
            }

            return Success(spot.ToSpotPutResponse());
        }

        public async Task<Result> DeleteSpot(long id)
        {
            if (!IsValidId(id))
                return Error<SpotPutResponse>(Errors.SpotIdIsInvalid);

            Spot spot;

            try { spot = await _repository.Read(id); }
            catch { return Error(Errors.SpotReadingFromDatabaseError);  }

            if (spot == null)
                return Error(Errors.SpotNotFoundError);

            try { await _repository.Delete(id); }
            catch { return Error(Errors.SpotsReadingFromDatabaseError);  }

            return Success();
        }

        private bool IsValidId(long id)
        {
            if (id < 1000000000000000 || id > 9999999999999999)
                return false;

            return true;
        }
    }
}
