using System;
using System.Collections.Generic;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Utils
{
    public static class RoleModelExtension
    {
        public static Permission ToPermissionEnum(this string permission)
        {
            return (Permission)Enum.Parse(typeof(Permission), permission);
        }
        
        public static List<Permission> ToPermissionList(this List<string> permissions)
        {
            return permissions.ConvertAll(permission => permission.ToPermissionEnum());
        }
        
        public static List<string> ToStringList(this List<Permission> permissions)
        {
            return permissions.ConvertAll(permission => permission.ToString());
        }
    }
}
