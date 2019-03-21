using System.Collections.Generic;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Tests.TestUtils.Builders
{
    public class RoleBuilder
    {
        private long _id = Generate.NewId();
        private string _roleName = Generate.NewString(15);
        private List<Permission> _permissions = new List<Permission>();

        public RoleBuilder() { }

        public Role Build()
        {
            return new Role
            {
                Id = _id,
                Name = _roleName,
                Permissions = _permissions
            };
        }

        public RoleBuilder WithId(long id)
        {
            _id = id;
            return this;
        }

        public RoleBuilder WithName(string name)
        {
            _roleName = name;
            return this;
        }
        
        public RoleBuilder WithPermissions(List<Permission> permissions)
        {
            _permissions = permissions;
            return this;
        }
    }
}