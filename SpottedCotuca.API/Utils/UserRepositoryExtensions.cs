using Google.Cloud.Datastore.V1;
using SpottedCotuca.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Utils
{
    public static class UserRepositoryExtensions
    {
        public static Entity ToEntity(this User user) => new Entity()
        {
            Key = user.Id.ToUserKey(),
            ["username"] = user.Username,
            ["password"] = user.Password,
            ["salt"] = user.Salt,
            ["role"] = user.Role
        };

        public static User ToUser(this Entity entity) => new User()
        {
            Id = entity.Key.Path.First().Id,
            Username = (string) entity["username"],
            Password = (string) entity["password"],
            Salt = (string) entity["salt"],
            Role= (string) entity["role"]
        };
    }
}
