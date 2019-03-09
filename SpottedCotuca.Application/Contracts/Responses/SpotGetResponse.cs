﻿using System;

namespace SpottedCotuca.Application.Contracts.Responses
{
    public class SpotGetResponse
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime PostDate { get; set; }
        public long FacebookId { get; set; }
        public long TwitterId { get; set; }
    }
}
