using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using SpottedCotuca.Application.Contracts.Validator;
using SpottedCotuca.Application.Services;

namespace SpottedCotuca.Application.Contracts.Requests.User
{
    public class UserPostRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Entities.Models.User ToUser()
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(this.Password);
            byte[] saltBytes = GenerateSaltBytes();

            byte[] hashedPassword = HashPassword(passwordBytes, saltBytes);
            return new Entities.Models.User()
            {
                Username = this.Username,
                Password = BitConverter.ToString(hashedPassword).Replace("-", String.Empty),
                Salt = BitConverter.ToString(saltBytes).Replace("-", String.Empty),
                Role = "moderator"
            };
        }

        private byte[] GenerateSaltBytes()
        {
            byte[] salt = new byte[64];
            new Random().NextBytes(salt);

            return salt;
        }

        private byte[] HashPassword(byte[] password, byte[] salt)
        {
            HashAlgorithm sha256 = SHA256.Create();
            HashAlgorithm sha512 = SHA512.Create();

            byte[] passwordHash = sha256.ComputeHash(password);
            byte[] saltHash = sha256.ComputeHash(salt);
            return sha512.ComputeHash(passwordHash.Concat(saltHash).ToArray());
        }
    }

    public class UserPostRequestValidator : AbstractValidator<UserPostRequest>
    {
        public UserPostRequestValidator()
        {
            RuleFor(request => request.Username)
                .NotEmpty()
                .WithCustomError(Errors.UserUsernameIsEmpty)
                .Length(4, 30)
                .WithCustomError(Errors.UserUsernameLength);

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithCustomError(Errors.UserPasswordIsEmpty)
                .MinimumLength(8)
                .WithCustomError(Errors.UserPasswordIsLesserThan8);
        }
    }
}
