using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;
using System;
using System.Security.Cryptography;
using Microsoft.AspNet.Cryptography.KeyDerivation;

namespace PayMeBack.Backend.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AppUser Create(string name, string email, string password)
        {
            var salt = GenerateSalt();
            var passwordHash = GeneratePasswordHash(password, salt);

            var user = new AppUser {
                Name = name,
                Email = email,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(salt),
                Creation = DateTime.Now
            };
            var result = _userRepository.Create(user);

            return user;
        }

        private byte[] GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(salt);
            }
            return salt;
        }

        private byte[] GeneratePasswordHash(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var passwordHashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return passwordHashBytes;
        }
    }
}
