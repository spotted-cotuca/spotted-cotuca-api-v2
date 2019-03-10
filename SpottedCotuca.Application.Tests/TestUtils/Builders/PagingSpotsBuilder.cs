using SpottedCotuca.Application.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedCotuca.Application.Tests.TestUtils.Builders
{
    public class PagingSpotsBuilder
    {
        private int _limit = 2;
        private int _offset = 1;
        private List<Spot> _spots = new List<Spot>();

        public PagingSpotsBuilder() { }

        public PagingSpots Build()
        {
            return new PagingSpots(_spots, _offset, _limit);
        }

        public PagingSpotsBuilder WithLimit(int limit)
        {
            _limit = limit;
            return this;
        }

        public PagingSpotsBuilder WithOffset(int offset)
        {
            _offset = offset;
            return this;
        }
        
        public PagingSpotsBuilder WithSpots(List<Spot> spots)
        {
            _spots = spots;
            return this;
        }
    }
}
