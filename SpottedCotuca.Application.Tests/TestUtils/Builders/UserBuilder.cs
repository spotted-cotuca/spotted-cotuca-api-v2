using SpottedCotuca.Application.Entities.Models;
using System;

namespace SpottedCotuca.Application.Tests.TestUtils.Builders
{
    public class UserBuilder
    {
        private long _id = Generate.NewId();
        private string _username = Generate.NewString(30);
        private string _password = Generate.NewString(30);
        private string _salt = Generate.NewString(15);
        private string _role = Generate.NewString(15);

        public User Build()
        {
            return new User
            {
                Id = _id,
                Username = _username,
                Password = _password,
                Salt = _salt,
                Role = _role
            };
        }

        public UserBuilder WithId(long id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder WithSalt(string salt)
        {
            _salt = salt;
            return this;
        }

        public UserBuilder WithRole(string role)
        {
            _role = role;
            return this;
        }
    }
}
