using FluentAssertions;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Services.Utils;
using SpottedCotuca.Application.Tests.TestUtils.Builders;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpottedCotuca.Application.Tests.Services
{
    [TestClass]
    [TestCategory("MapperExtensions")]
    public class MapperExtensionsTests
    {
        private Spot _spot;
        private List<Spot> _spots;
        private int _offset = 0;
        private int _limit = 50;

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

            _spots = new List<Spot> { _spot, _spot };
        }

        [TestMethod]
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

        [TestMethod]
        public void ShouldMapSpotsOffsetAndLimitToSpotsGetResponse()
        {
            var response = _spots.ToSpotsGetResponse(_offset, _limit);

            response.Paging.Offset.Should().Be(_offset);
            response.Paging.Limit.Should().Be(_limit);
            response.Paging.Count.Should().Be(_spots.Count);
            response.Spots.Count.Should().Be(_spots.Count);
        }

        [TestMethod]
        public void ShouldMapSpotToSpotPostResponse()
        {
            var response = _spot.ToSpotPostResponse();

            response.Id.Should().Be(_spot.Id);
        }

        [TestMethod]
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
