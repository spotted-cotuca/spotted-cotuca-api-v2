using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Models
{
    public class User
    {
        public String Username { get; private set; }
        public String Email { get; private set; }
        public String Password { get; private set; }
        public String Role { get; private set; }
    }
}
