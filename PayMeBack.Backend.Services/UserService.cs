using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;
using System;
using System.Security.Cryptography;
using Microsoft.AspNet.Cryptography.KeyDerivation;
using System.Collections.Generic;
using JWT;

namespace PayMeBack.Backend.Services
{
    public class UserService : IUserService
    {
        private IGenericRepository<AppUser> _userRepository;
        private IDateTimeProvider _dateTimeProvider;

        public UserService(IGenericRepository<AppUser> userRepository, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
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

        private const string loginFailedMessage = "The email / password provided does not match any user.";
        private const string _secretJwtKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        public UserAndToken Login(string email, string password)
        {
            var user = _userRepository.GetFirst(u => u.Email == email);
            if (user == null)
            {
                throw new SecurityException(loginFailedMessage);
            }

            var requestPasswordHash = Convert.ToBase64String(GeneratePasswordHash(password, Convert.FromBase64String(user.PasswordSalt)));
            if (requestPasswordHash != user.PasswordHash)
            {
                throw new SecurityException(loginFailedMessage);
            }

            var payload = new Dictionary<string, object>() {
                { "userId", user.Id },
                { "exp", ConvertToSecondsSinceEpoch(_dateTimeProvider.Now().AddDays(30)) }
            };

            var token = JsonWebToken.Encode(payload, _secretJwtKey, JwtHashAlgorithm.HS256);

            return new UserAndToken { User = user, Token = token };
        }

        private int ConvertToSecondsSinceEpoch(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return (int)Math.Floor(diff.TotalSeconds);
        }

        public AppUser GetUserForToken(string token)
        {
            IDictionary<string, object> payload;
            try
            {
                payload = JsonWebToken.DecodeToObject(token, _secretJwtKey) as IDictionary<string, object>;
            }
            catch
            {
                throw new SecurityException("The token is not valid.");
            }

            var userId = Convert.ToInt32(payload["userId"]);

            var user = _userRepository.GetById(userId);

            return user;
        }
    }
}
