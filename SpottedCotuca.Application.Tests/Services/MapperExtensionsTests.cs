using FluentAssertions;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Services.Utils;
using SpottedCotuca.Application.Tests.TestUtils.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace SpottedCotuca.Application.Tests.Services
{
    public class MapperExtensionsTests
    {
        private Spot _spot;
        private PagingSpots _pagingSpots;

        public MapperExtensionsTests()
        {
            _spot = new SpotBuilder()
                            .WithId(1234567890123456)
                            .WithMessage("\"Hey, isso é um Spot de teste!\"")
                            .WithStatus(Status.Approved)
                            .WithPostDate(DateTime.Now)
                            .WithFacebookId(0171419300152494)
                            .WithTwitterId(9699889487548702)
                            .Build();

            _pagingSpots = new PagingSpotsBuilder()
                            .WithLimit(50)
                            .WithOffset(1)
                            .WithSpots(new List<Spot> { _spot, _spot })
                            .Build();
        }

        [Fact(DisplayName = "Should map Spot to SpotGetResponse")]
        public void ShouldMapSpotToSpotGetResponse()
        {
            var response = _spot.ToSpotGetResponse();

            response.Id.Should().Be(_spot.Id);
            response.Message.Should().Be(_spot.Message);
            response.Status.Should().Be(_spot.Status.ToString());
            response.PostDate.Should().Be(_spot.PostDate);
            response.FacebookId.Should().Be(_spot.FacebookId);
            response.TwitterId.Should().Be(_spot.TwitterId);
        }

        [Fact(DisplayName = "Should map PagingSpots to SpotsGetResponse")]
        public void ShouldMapPagingSpotsToSpotsGetResponse()
        {
            var response = _pagingSpots.ToSpotsGetResponse();

            response.Paging.Limit.Should().Be(_pagingSpots.Limit);
            response.Paging.Offset.Should().Be(_pagingSpots.Offset);
            response.Paging.Count.Should().Be(_pagingSpots.Spots.Count);
            response.Spots.Count.Should().Be(_pagingSpots.Spots.Count);
        }

        [Fact(DisplayName = "Should map Spot to SpotPostResponse")]
        public void ShouldMapSpotToSpotPostResponse()
        {
            var response = _spot.ToSpotPostResponse();

            response.Id.Should().Be(_spot.Id);
        }

        [Fact(DisplayName = "Should map Spot to SpotPutResponse")]
        public void ShouldMapSpotToSpotPutResponse()
        {
            var response = _spot.ToSpotPutResponse();

            response.Id.Should().Be(_spot.Id);
            response.Message.Should().Be(_spot.Message);
            response.Status.Should().Be(_spot.Status.ToString());
            response.FacebookId.Should().Be(_spot.FacebookId);
            response.TwitterId.Should().Be(_spot.TwitterId);
        }
    }
}
