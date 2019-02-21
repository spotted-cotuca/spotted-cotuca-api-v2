using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Repositories
{
    public static class SpotsRepositoryExtensions
    {
        public static Key ToKey(this long id) =>
            new Key().WithElement("Spot", id);

        public static long ToId(this Key key) => key.Path.First().Id;

        public static Entity ToEntity(this Spot spot) => new Entity()
        {
            Key = spot.Id.ToKey(),
            ["message"] = spot.Message,
            ["status"] = (int)spot.Status,
            ["date"] = spot.PostDate.ToUniversalTime(),
            ["fbPostId"] = spot.FacebookId,
            ["ttPostId"] = spot.TwitterId
        };

        public static Models.Spot ToSpot(this Entity entity) => new Spot()
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
