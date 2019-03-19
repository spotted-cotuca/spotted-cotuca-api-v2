using FluentAssertions;
using Google.Cloud.Datastore.V1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpottedCotuca.Aplication.Repositories.Datastore;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Tests.TestUtils.Builders;
using SpottedCotuca.Application.Utils;
using System;

namespace SpottedCotuca.Application.Tests.Repositories
{
    [TestCategory("DatastoreSpotRepository")]
    public abstract class DatastoreSpotRepositoryTests
    {
        protected Spot _spot;
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
            _spot.Id = responseSpot.Id;

            var fetchedSpot = _provider.Db.Lookup(responseSpot.Id.ToSpotKey()).ToSpot();

            fetchedSpot.Id.Should().Be(_spot.Id);
            fetchedSpot.Message.Should().Be(_spot.Message);
            fetchedSpot.Status.Should().Be(_spot.Status);
            fetchedSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(_spot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            fetchedSpot.FacebookId.Should().Be(_spot.FacebookId);
            fetchedSpot.TwitterId.Should().Be(_spot.TwitterId);
        }
    }

    [TestClass]
    public class DatastoreSpotRepositoryUpdateTests : DatastoreSpotRepositoryTests
    {
        [TestMethod]
        public void ShouldUpdateTheSpot()
        {
            _spot.Status = Status.Rejected;
            _repo.Update(_spot).GetAwaiter().GetResult();

            var fetchedSpot = _provider.Db.Lookup(_spot.Id.ToSpotKey()).ToSpot();

            fetchedSpot.Id.Should().Be(_spot.Id);
            fetchedSpot.Message.Should().Be(_spot.Message);
            fetchedSpot.Status.Should().Be(_spot.Status);
            fetchedSpot.PostDate.ToString("dd/MM/yyyy HH:mm").Should()
                .Be(_spot.PostDate.ToString("dd/MM/yyyy HH:mm"));
            fetchedSpot.FacebookId.Should().Be(_spot.FacebookId);
            fetchedSpot.TwitterId.Should().Be(_spot.TwitterId);
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
