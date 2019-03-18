using FluentAssertions;
using Google.Cloud.Datastore.V1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpottedCotuca.Aplication.Repositories.Datastore;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Tests.TestUtils.Builders;
using SpottedCotuca.Application.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpottedCotuca.Application.Tests.Repositories
{
    [TestClass]
    public class DatastoreSpotRepositoryTests
    {
        private static Spot _spot;
        private static DatastoreSpotRepository _repo;
        private static DatastoreProvider _provider;

        static DatastoreSpotRepositoryTests()
        {
            _provider = new TestDatastoreProvider();
            _repo = new DatastoreSpotRepository(_provider);

            var spots = _provider.Db.RunQuery(new Query("Spot"));
            _provider.Db.Delete(spots.Entities);

            _spot = new SpotBuilder()
                            .WithId(1234567890123456)
                            .WithMessage("\"Hey, isso é um Spot de teste!\"")
                            .WithStatus(Status.Approved)
                            .WithPostDate(DateTime.Now)
                            .WithFacebookId(0171419300152494)
                            .WithTwitterId(9699889487548702)
                            .Build();
        }

        [TestMethod]
        public void TestSpotRepository()
        {
            Task.Run(async () =>
            {
                await ShouldCreateTheSpot();
                await ShouldUpdateTheSpot();
                await ShouldDeleteTheSpot();
            });
        }
        
        private static async Task ShouldCreateTheSpot()
        {
            var responseSpot = await _repo.Create(_spot);
            var fetchedSpot = await _repo.Read(responseSpot.Id);
            _spot.Id = responseSpot.Id;

            fetchedSpot.Id.Should().Be(responseSpot.Id);
            fetchedSpot.Message.Should().Be(_spot.Message);
            fetchedSpot.Status.Should().Be(_spot.Status);
            fetchedSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(_spot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            fetchedSpot.FacebookId.Should().Be(_spot.FacebookId);
            fetchedSpot.TwitterId.Should().Be(_spot.TwitterId);
        }

        private static async Task ShouldUpdateTheSpot()
        {
            _spot.Status = Status.Rejected;
            await _repo.Update(_spot);
            var fetchedSpot = await _repo.Read(_spot.Id);

            fetchedSpot.Id.Should().Be(_spot.Id);
            fetchedSpot.Message.Should().Be(_spot.Message);
            fetchedSpot.Status.Should().Be(_spot.Status);
            fetchedSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(_spot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            fetchedSpot.FacebookId.Should().Be(_spot.FacebookId);
            fetchedSpot.TwitterId.Should().Be(_spot.TwitterId);
        }

        private static async Task ShouldDeleteTheSpot()
        {
            await _repo.Delete(_spot.Id);
            var fetchedSpot = await _repo.Read(_spot.Id);

            fetchedSpot.Should().Be(null);
        }
    }
}
