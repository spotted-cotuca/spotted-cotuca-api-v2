using System;

namespace SpottedCotuca.Application.Contracts.Responses
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
