using System.Collections.Generic;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Entities.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
