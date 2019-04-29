using SpottedCotuca.Application.Contracts.Responses.Role;
using SpottedCotuca.Application.Contracts.Responses.Spot;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.Application.Services.Utils
{
    public static class RoleMapperExtensions
    {
        public static RoleGetResponse ToRoleGetResponse(this Role role)
        {
            return new RoleGetResponse
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = role.Permissions.ConvertAll(permission => permission.ToString())
            };
        }

        public static RolePostResponse ToRolePostResponse(this Role role)
        {
            return new RolePostResponse { Id = role.Id };
        }

        public static RolePutResponse ToRolePutResponse(this Role role)
        {
            return new RolePutResponse
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = role.Permissions.ConvertAll(permission => permission.ToString())
            };
        }
    }
}
