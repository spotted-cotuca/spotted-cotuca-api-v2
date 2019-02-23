using Google.Cloud.Datastore.V1;
using System;
using System.Linq;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Utils
{
    public static class SpotsRepositoryExtensions
    {
        public static Entity ToEntity(this Spot spot) => new Entity()
        {
            Key = spot.Id.ToSpotKey(),
            ["message"] = spot.Message,
            ["status"] = (int)spot.Status,
            ["date"] = spot.PostDate.ToUniversalTime(),
            ["fbPostId"] = spot.FacebookId,
            ["ttPostId"] = spot.TwitterId
        };

        public static Spot ToSpot(this Entity entity) => new Spot()
        {
            Id = entity.Key.Path.First().Id,
            Message = (string)entity["message"],
            Status = (Status)(int)entity["status"],
            PostDate = (DateTime)entity["date"],
            FacebookId = (long)entity["fbPostId"],
            TwitterId = (long)entity["ttPostId"]
        };
    }
}
