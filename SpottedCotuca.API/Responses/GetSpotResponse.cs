using SpottedCotuca.API.Models;
using System;

namespace SpottedCotuca.API.Responses
{
    public class GetSpotResponse
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public DateTime PostDate { get; set; }
        public long FacebookId { get; set; }
        public long TwitterId { get; set; }
    }
}
