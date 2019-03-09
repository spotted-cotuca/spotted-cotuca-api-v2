using System;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.Application.Utils
{
    public static class SpotModelExtension
    {
        public static Status ToStatusEnum(this string status)
        {
            return (Status)Enum.Parse(typeof(Status), status);
        }
    }
}
