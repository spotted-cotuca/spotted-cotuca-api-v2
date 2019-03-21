using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Utils
{
    public static class RoleRepositoryExtensions
    {
        public static Entity ToEntity(this Role role) => new Entity()
        {
            Key = role.Id.ToRoleKey(),
            ["name"] = role.Name,
            ["permissions"] = new ArrayValue()
            {
                Values = 
                {
                    role.Permissions.ConvertAll(permission => new Value(permission.ToString()))
                }
            },
        };

        public static Role ToRole(this Entity entity)
        {
            if (entity == null)
                return null;

            return new Role()
            {
                Id = entity.Key.Path.First().Id,
                Name = (string)entity["name"],
                Permissions = entity["permissions"]
                    .ArrayValue.Values.Select(x => x.StringValue.ToPermissionEnum()).ToList()
            };
        }
    }
}
