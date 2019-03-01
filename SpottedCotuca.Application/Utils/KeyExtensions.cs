using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.Application.Utils
{
    public static class KeyExtensions
    {
        public static Key ToSpotKey(this long id) =>
            new Key().WithElement("Spot", id);

        public static Key ToUserKey(this long id) =>
            new Key().WithElement("User", id);

        public static long ToId(this Key key) => key.Path.First().Id;
    }
}
