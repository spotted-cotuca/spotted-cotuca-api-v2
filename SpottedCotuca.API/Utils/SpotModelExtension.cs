using System;
using SpottedCotuca.API.Models;

namespace SpottedCotuca.API.Utils
{
    public static class SpotModelExtension
    {
        public static Status ToStatus(this string status)
        {
            return (Status)Enum.Parse(typeof(Status), status);
        }
    }
}
