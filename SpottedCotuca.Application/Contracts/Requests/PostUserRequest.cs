using FluentValidation;
using SpottedCotuca.Application.Entities.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SpottedCotuca.Application.Contracts.Requests
{
    public class PostUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User ToUser()
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(this.Password);
            byte[] saltBytes = GenerateSaltBytes();

            byte[] hashedPassword = HashPassword(passwordBytes, saltBytes);
            return new User()
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

    public class PostUserRequestValidator : AbstractValidator<PostUserRequest>
    {
        public PostUserRequestValidator()
        {
            RuleFor(request => request.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty.")
                .Length(4, 30)
                .WithMessage("Username length has to be between 4 and 30 characters.");

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .MinimumLength(8)
                .WithMessage("Password length has to be at least 8 characters.");
        }
    }
}
