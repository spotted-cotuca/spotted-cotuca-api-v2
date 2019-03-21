using System.Threading.Tasks;
using FluentAssertions;
using Google.Cloud.Datastore.V1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpottedCotuca.Aplication.Repositories.Datastore;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Tests.TestUtils.Builders;

namespace SpottedCotuca.Application.Tests.Repositories
{
    [TestClass]
    [TestCategory("DatastoreUserRepository")]
    public class DatastoreUserRepositoryTests
    {
        private static User _user;
        private static DatastoreUserRepository _repo;
        private static DatastoreProvider _provider;

        static DatastoreUserRepositoryTests()
        {
            _provider = new TestDatastoreProvider();
            _repo = new DatastoreUserRepository(_provider);

            var users = _provider.Db.RunQuery(new Query("User"));
            _provider.Db.Delete(users.Entities);

            _user = new UserBuilder()
                        .WithId(1234)
                        .WithUsername("user.name")
                        .WithPassword("nicepassword")
                        .WithSalt("ABCDEFG")
                        .WithRole("admin")
                        .Build();
        }

        [TestMethod]
        public void TestUserRepository()
        {
            Task.Run(async () =>
            {
                await ShouldUpdateTheUser();
                await ShouldCreateTheUser();
                await ShouldDeleteTheUser();
            });
        }

        private static async Task ShouldCreateTheUser()
        {
            var responseUser = await _repo.Create(_user);
            var fetchedUser = await _repo.Read(responseUser.Username);
            _user.Id = responseUser.Id;

            fetchedUser.Id.Should().Be(responseUser.Id);
            fetchedUser.Username.Should().Be(_user.Username);
            fetchedUser.Password.Should().Be(_user.Password);
            fetchedUser.Salt.Should().Be(_user.Salt);
            fetchedUser.Role.Should().Be(_user.Role);
        }

        private static async Task ShouldUpdateTheUser()
        {
            _user.Role = "Another Role";
            var responseUser = await _repo.Update(_user);
            var fetchedUser = await _repo.Read(_user.Username);

            fetchedUser.Id.Should().Be(responseUser.Id);
            fetchedUser.Username.Should().Be(_user.Username);
            fetchedUser.Password.Should().Be(_user.Password);
            fetchedUser.Salt.Should().Be(_user.Salt);
            fetchedUser.Role.Should().Be("Another Role");
        }

        private static async Task ShouldDeleteTheUser()
        {
            await _repo.Delete(_user.Username);
            var fetchedUser = await _repo.Read(_user.Username);

            fetchedUser.Should().Be(null);
        }
    }
}
