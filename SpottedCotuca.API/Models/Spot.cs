using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Models
{
    public class Spot
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public Status Status { get; set; }
        public DateTime PostDate { get; set; }
        public long FacebookId { get; set; }
        public long TwitterId { get; set; }
    }

    public enum Status
    {
        Rejected = -1,
        Pending = 0,
        Approved = 1,
    }
}
