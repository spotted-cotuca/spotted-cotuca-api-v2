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
using System.Linq;

namespace SpottedCotuca.Application.Tests.Repositories
{
    [TestCategory("DatastoreSpotRepository")]
    public abstract class DatastoreSpotRepositoryTests
    {
        protected static Spot _spot;
        protected static DatastoreSpotRepository _repo;
        protected static DatastoreProvider _provider;

        public DatastoreSpotRepositoryTests()
        {
            _provider = new TestDatastoreProvider();
            _repo = new DatastoreSpotRepository(_provider);
        }

        [TestInitialize]
        public void Initialize()
        {
            BuildSpot();
            CreateSpotOnDatastore();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _provider.Db.Delete(_spot.Id.ToSpotKey());
        }

        protected void BuildSpot()
        {
            _spot = new SpotBuilder()
                .WithMessage("\"Hey, isso é um Spot de teste!\"")
                .WithStatus(Status.Approved)
                .WithPostDate(DateTime.Now)
                .WithFacebookId(0171419300152494)
                .WithTwitterId(9699889487548702)
                .Build();
        }

        protected void CreateSpotOnDatastore()
        {
            using (DatastoreTransaction transaction = _provider.Db.BeginTransaction())
            {
                var entity = _spot.ToEntity();
                entity.Key = _provider.Db.CreateKeyFactory("Spot").CreateIncompleteKey();
                transaction.Insert(entity);

                transaction.Commit();
                _spot.Id = entity.Key.ToId();
            }
        }
    }

    [TestClass]
    public class DatastoreSpotRepositoryReadTests : DatastoreSpotRepositoryTests
    {
        [TestMethod]
        public void ShouldReadTheSpot()
        {
            var responseSpot = _repo.Read(_spot.Id).Result;

            var fetchedSpot = _provider.Db.Lookup(responseSpot.Id.ToSpotKey()).ToSpot();

            fetchedSpot.Id.Should().Be(responseSpot.Id);
            fetchedSpot.Message.Should().Be(responseSpot.Message);
            fetchedSpot.Status.Should().Be(responseSpot.Status);
            fetchedSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(responseSpot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            fetchedSpot.FacebookId.Should().Be(responseSpot.FacebookId);
            fetchedSpot.TwitterId.Should().Be(responseSpot.TwitterId);
        }
    }

    [TestClass]
    public class DatastoreSpotsRepositoryReadTests : DatastoreSpotRepositoryTests
    {
        private static List<Spot> _spots;

        [TestInitialize]
        new public void Initialize()
        {
            BuildSpots();
            CreateSpotsOnDatastore();
        }

        [TestMethod]
        public void ShouldReadTheSpots()
        {
            var responseSpots = _repo.Read(Status.Approved, 0, 1).Result;

            var query = new Query("Spot")
            {
                Filter = Filter.And(Filter.Equal("status", (int)Status.Approved)),
                Order = { { "date", PropertyOrder.Types.Direction.Descending} },
                Offset = 0,
                Limit = 1
            };

            var fetchedSpots = _provider.Db.RunQuery(query).Entities.Select(entity => entity.ToSpot()).ToList();

            responseSpots.Spots.Count.Should().Be(fetchedSpots.Count);

            responseSpots.Spots.FirstOrDefault().Id.Should().Be(fetchedSpots.FirstOrDefault().Id);
            responseSpots.Spots.FirstOrDefault().Message.Should().Be(fetchedSpots.FirstOrDefault().Message);
            responseSpots.Spots.FirstOrDefault().Status.Should().Be(fetchedSpots.FirstOrDefault().Status);
            responseSpots.Spots.FirstOrDefault().PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(fetchedSpots.FirstOrDefault().PostDate.ToString("dd/MM/yyyy HH:mm"));
            responseSpots.Spots.FirstOrDefault().FacebookId.Should().Be(fetchedSpots.FirstOrDefault().FacebookId);
            responseSpots.Spots.FirstOrDefault().TwitterId.Should().Be(fetchedSpots.FirstOrDefault().TwitterId);
        }

        [TestCleanup()]
        new public void Cleanup()
        {
            foreach (Spot spot in _spots)
            {
                _provider.Db.Delete(_spot.Id.ToSpotKey());
            }
        }

        private void BuildSpots()
        {
            _spots = new List<Spot>
            {
                new SpotBuilder()
                    .WithMessage("\"Hey, isso é o primeiro Spot de teste!\"")
                    .WithStatus(Status.Approved)
                    .WithPostDate(DateTime.Now)
                    .WithFacebookId(0171419300152495)
                    .WithTwitterId(9699889487548703)
                    .Build(),

                new SpotBuilder()
                    .WithMessage("\"Hey, isso é o segundo Spot de teste!\"")
                    .WithStatus(Status.Approved)
                    .WithPostDate(DateTime.Now)
                    .WithFacebookId(0171419300152496)
                    .WithTwitterId(9699889487548704)
                    .Build()
            };
        }

        private void CreateSpotsOnDatastore()
        {
            foreach (Spot spot in _spots)
            {
                using (DatastoreTransaction transaction = _provider.Db.BeginTransaction())
                {
                    var entity = spot.ToEntity();
                    entity.Key = _provider.Db.CreateKeyFactory("Spot").CreateIncompleteKey();
                    transaction.Insert(entity);

                    transaction.Commit();
                    spot.Id = entity.Key.ToId();
                }
            }
        }
    }

    [TestClass]
    public class DatastoreSpotRepositoryCreateTests : DatastoreSpotRepositoryTests
    {
        [TestInitialize]
        new public void Initialize()
        {
            BuildSpot();
        }

        [TestMethod]
        public void ShouldCreateTheSpot()
        {
            var responseSpot = _repo.Create(_spot).Result;

            var fetchedSpot = _provider.Db.Lookup(responseSpot.Id.ToSpotKey()).ToSpot();

            responseSpot.Message.Should().Be(_spot.Message);
            responseSpot.Status.Should().Be(_spot.Status);
            responseSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(_spot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            responseSpot.FacebookId.Should().Be(_spot.FacebookId);
            responseSpot.TwitterId.Should().Be(_spot.TwitterId);

            fetchedSpot.Id.Should().Be(responseSpot.Id);
            fetchedSpot.Message.Should().Be(responseSpot.Message);
            fetchedSpot.Status.Should().Be(responseSpot.Status);
            fetchedSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(responseSpot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            fetchedSpot.FacebookId.Should().Be(responseSpot.FacebookId);
            fetchedSpot.TwitterId.Should().Be(responseSpot.TwitterId);
        }
    }

    [TestClass]
    public class DatastoreSpotRepositoryUpdateTests : DatastoreSpotRepositoryTests
    {
        [TestMethod]
        public void ShouldUpdateTheSpot()
        {
            _spot.Status = Status.Rejected;
            var responseSpot = _repo.Update(_spot).Result;

            var fetchedSpot = _provider.Db.Lookup(_spot.Id.ToSpotKey()).ToSpot();

            responseSpot.Id.Should().Be(_spot.Id);
            responseSpot.Message.Should().Be(_spot.Message);
            responseSpot.Status.Should().Be(_spot.Status);
            responseSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(_spot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            responseSpot.FacebookId.Should().Be(_spot.FacebookId);
            responseSpot.TwitterId.Should().Be(_spot.TwitterId);

            fetchedSpot.Id.Should().Be(responseSpot.Id);
            fetchedSpot.Message.Should().Be(responseSpot.Message);
            fetchedSpot.Status.Should().Be(responseSpot.Status);
            fetchedSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(responseSpot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            fetchedSpot.FacebookId.Should().Be(responseSpot.FacebookId);
            fetchedSpot.TwitterId.Should().Be(responseSpot.TwitterId);
        }
    }

    [TestClass]
    public class DatastoreSpotRepositoryDeleteTests : DatastoreSpotRepositoryTests
    {
        [TestMethod]
        public void ShouldDeleteTheSpot()
        {
            _repo.Delete(_spot.Id).GetAwaiter().GetResult();

            var fetchedSpot = _provider.Db.Lookup(_spot.Id.ToSpotKey()).ToSpot();

            fetchedSpot.Should().Be(null);
        }
    }
}
