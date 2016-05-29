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
        private IGenericRepository<AppUser> _userRepository;

        public UserService(IGenericRepository<AppUser> userRepository)
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
            var result = _userRepository.Insert(user);

            return user;
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(salt);
            }
            return salt;
        }

        private byte[] GeneratePasswordHash(string password, byte[] salt)
        {
            var passwordHashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return passwordHashBytes;
        }

        public UserAndToken Login(string email, string password)
        {
            var user = _userRepository.GetFirst(u => u.Email == email);

            var requestPasswordHash = Convert.ToBase64String(GeneratePasswordHash(password, Convert.FromBase64String(user.PasswordSalt)));

            if (requestPasswordHash != user.PasswordHash)
            {
                throw new SecurityException("The password does not match.");
            }

            return new UserAndToken { User = user, Token = "ERE4F8ZER65FS4D6F8ERZF68HODM1VT" };
        }
    }
}
