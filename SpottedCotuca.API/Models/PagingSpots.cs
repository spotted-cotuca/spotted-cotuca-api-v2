using System.Collections.Generic;

namespace SpottedCotuca.API.Models
{
    public class PagingSpots
    {
        public int Limit { get; private set; }
        public int Offset { get; private set; }
        public int Count { get => Spots.Count; }
        public List<Spot> Spots { get; private set; } = new List<Spot>();

        public PagingSpots(List<Spot> spots, int offset, int limit)
        {
            Limit = limit;
            Offset = offset;
            Spots = spots;
        }
    }
}
