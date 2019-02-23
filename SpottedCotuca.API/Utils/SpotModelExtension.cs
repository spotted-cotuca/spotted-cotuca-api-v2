using System;
using SpottedCotuca.API.Models;
using SpottedCotuca.API.Exceptions;

namespace SpottedCotuca.API.Utils
{
    public static class SpotModelExtension
    {
        public static Status ToStatus(this string status)
        {
            try { return (Status)Enum.Parse(typeof(Status), status); }

            catch { throw new InvalidStatusException("\"status\" must be: \"rejected\", \"pending\" or \"approved\" "); }
        }
    }
}
