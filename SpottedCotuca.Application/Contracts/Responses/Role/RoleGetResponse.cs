using System;
using System.Collections.Generic;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Contracts.Responses.Role
{
    public class RoleGetResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}
