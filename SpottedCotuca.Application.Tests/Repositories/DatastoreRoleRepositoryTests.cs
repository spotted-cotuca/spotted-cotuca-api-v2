using FluentAssertions;
using Google.Cloud.Datastore.V1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpottedCotuca.Aplication.Repositories.Datastore;
using SpottedCotuca.Application.Data.Repositories.Datastore;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Tests.TestUtils.Builders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Tests.Repositories
{
    [TestClass]
    public class DatastoreRoleRepositoryTests
    {
        private static Role _role;
        private static DatastoreRoleRepository _repo;
        private static DatastoreProvider _provider;

        static DatastoreRoleRepositoryTests()
        {
            _provider = new TestDatastoreProvider();
            _repo = new DatastoreRoleRepository(_provider);

            var spots = _provider.Db.RunQuery(new Query("Role"));
            _provider.Db.Delete(spots.Entities);

            _role = new RoleBuilder()
                        .WithId(123456)
                        .WithName("admin")
                        .WithPermissions(new List<Permission>())
                        .Build();
        }

        [TestMethod]
        public void TestRoleRepository()
        {
            Task.Run(async () =>
            {
                await ShouldCreateTheRole();
                await ShouldUpdateTheRole();
                await ShouldDeleteTheRole();
            });
        }
        
        private static async Task ShouldCreateTheRole()
        {
            var responseRole = await _repo.Create(_role);
            var fetchedRole = await _repo.Read(responseRole.Name);
            _role.Id = responseRole.Id;

            fetchedRole.Id.Should().Be(responseRole.Id);
            fetchedRole.Name.Should().Be(_role.Name);
            fetchedRole.Permissions.Count.Should().Be(_role.Permissions.Count);
        }

        private static async Task ShouldUpdateTheRole()
        {
            var removedPermission = _role.Permissions[0];
            _role.Permissions.RemoveAt(0);
            await _repo.Update(_role);
            var fetchedRole = await _repo.Read(_role.Name);

            fetchedRole.Id.Should().Be(_role.Id);
            fetchedRole.Name.Should().Be(_role.Name);
            fetchedRole.Permissions.Should().NotContain(removedPermission);
            fetchedRole.Permissions.Count.Should().Be(_role.Permissions.Count);
        }

        private static async Task ShouldDeleteTheRole()
        {
            await _repo.Delete(_role.Name);
            var fetchedRole = await _repo.Read(_role.Name);

            fetchedRole.Should().Be(null);
        }
    }
}
