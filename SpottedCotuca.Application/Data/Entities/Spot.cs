using System;

namespace SpottedCotuca.Application.Entities.Models
{
    public class Spot
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public Status Status { get; set; }
        public DateTime PostDate {
            get => _postDate;
            set => _postDate = value.ToUniversalTime();
        }
        public long FacebookId { get; set; }
        public long TwitterId { get; set; }

        private DateTime _postDate;
    }

    public enum Status
    {
        Rejected = -1,
        Pending = 0,
        Approved = 1,
    }
}
