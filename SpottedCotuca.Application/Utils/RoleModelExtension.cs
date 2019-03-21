using System;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Utils
{
    public static class RoleModelExtension
    {
        public static Permission ToPermissionEnum(this string permission)
        {
            return (Permission)Enum.Parse(typeof(Permission), permission);
        }
    }
}
