using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpottedCotuca.API.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }

        public class UserSignupRequest
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
    }
}
