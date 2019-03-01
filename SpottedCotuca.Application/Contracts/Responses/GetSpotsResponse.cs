using System.Collections.Generic;

namespace SpottedCotuca.Application.Contracts.Responses
{
    public class GetSpotsResponse
    {
        public List<GetSpotResponse> Spots { get; set; }
        public PagingResponse Paging { get; set; }
    }

    public class PagingResponse
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
    }
}
