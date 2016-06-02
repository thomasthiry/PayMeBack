using JWT;
using JWT.DNX.Json.Net;
using Moq;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace PayMeBack.Backend.Services.Tests
{
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IGenericRepository<AppUser>> _userRepositoryMock;
        private Mock<IDateTimeProvider> _dateTimeProviderMock;
        private const string _secretJwtKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<AppUser>>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _dateTimeProviderMock.Setup(d => d.Now()).Returns(new DateTime(2020,6,1,12,0,0, DateTimeKind.Utc));
            _userService = new UserService(_userRepositoryMock.Object, _dateTimeProviderMock.Object);
            JsonWebToken.JsonSerializer = new JsonNetJWTSerializer();
        }

        [Fact]
        public void Create_CallsRepoWithHashAndSaltAndReturnsCreatedUser()
        {
            var password = "MyPass1";
            var userToCreate = new AppUser { Name = "John Smith", Email = "john@gmail.com" };

            _userRepositoryMock.Setup(r => r.Insert(It.Is<AppUser>(
                u =>
                    u.Name == userToCreate.Name &&
                    u.PasswordHash.Length > 1 &&
                    u.PasswordSalt.Length > 1 &&
                    u.Creation > DateTime.Now.AddHours(-1)
                ))).Returns(userToCreate);

            var createdUser = _userService.Create(userToCreate.Name, userToCreate.Email, password);

            Assert.Equal(userToCreate.Name, createdUser.Name);
            _userRepositoryMock.VerifyAll();
        }

        [Fact]
        public void Login_UserExistsAndPasswordMatch_ReturnsUserAndToken()
        {
            var password = "MyPass1";
            var userStub = new AppUser { Id = 3, Name = "John Smith", Email = "john@gmail.com", PasswordHash = "YkxknXOgltAnhEhO6yjm6EOwIVMO7NIF02x1Zcj99kA=", PasswordSalt = "JjsV9YytLSuZcQIsrIk6cg==" };

            _userRepositoryMock.Setup(r => r.GetFirst(It.IsAny<Expression<Func<AppUser, bool>>>())).Returns(userStub);

            var userAndToken = _userService.Login(userStub.Email, password);

            Assert.Equal(userStub.Name, userAndToken.User.Name);
            Assert.InRange(userAndToken.Token.Length, 1, int.MaxValue);
        }

        [Fact]
        public void Login_UserExistsAndPasswordDoesNotMatch_ThrowsException()
        {
            var password = "WRONG_PASSWORD";
            var userStub = new AppUser { Id = 3, Name = "John Smith", Email = "john@gmail.com", PasswordHash = "YkxknXOgltAnhEhO6yjm6EOwIVMO7NIF02x1Zcj99kA=", PasswordSalt = "JjsV9YytLSuZcQIsrIk6cg==" };

            _userRepositoryMock.Setup(r => r.GetFirst(It.IsAny<Expression<Func<AppUser, bool>>>())).Returns(userStub);

            Assert.Throws<SecurityException>(() => _userService.Login(userStub.Email, password));
        }

        [Fact]
        public void Login_UserDoesNotExist_ThrowsException()
        {
            _userRepositoryMock.Setup(r => r.GetFirst(It.IsAny<Expression<Func<AppUser, bool>>>())).Returns((AppUser)null);

            Assert.Throws<SecurityException>(() => _userService.Login("john@gmail.com", "MyPass1"));
        }

        [Fact]
        public void Login_GeneratesProperToken()
        {
            var password = "MyPass1";
            var userStub = new AppUser { Id = 3, Name = "John Smith", Email = "john@gmail.com", PasswordHash = "YkxknXOgltAnhEhO6yjm6EOwIVMO7NIF02x1Zcj99kA=", PasswordSalt = "JjsV9YytLSuZcQIsrIk6cg==" };

            _userRepositoryMock.Setup(r => r.GetFirst(It.IsAny<Expression<Func<AppUser, bool>>>())).Returns(userStub);

            var userAndToken = _userService.Login(userStub.Email, password);

            Assert.Equal("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjMsImV4cCI6MTU5MzYwNDgwMH0.9owMe3syRA308UNlgDghBnmodrA5FrcTHh_IAf7tRI8", userAndToken.Token);
        }

        [Fact]
        public void Login_GeneratedTokenContainsExpirationClaimIn30Days()
        {
            var password = "MyPass1";
            var userStub = new AppUser { Id = 3, Name = "John Smith", Email = "john@gmail.com", PasswordHash = "YkxknXOgltAnhEhO6yjm6EOwIVMO7NIF02x1Zcj99kA=", PasswordSalt = "JjsV9YytLSuZcQIsrIk6cg==" };

            _userRepositoryMock.Setup(r => r.GetFirst(It.IsAny<Expression<Func<AppUser, bool>>>())).Returns(userStub);

            var userAndToken = _userService.Login(userStub.Email, password);

            var payload = JsonWebToken.DecodeToObject(userAndToken.Token, _secretJwtKey) as IDictionary<string, object>;

            var expiryDate = ConvertFromSecondsSinceEpoch(Convert.ToInt32(payload["exp"]));

            Assert.Equal(_dateTimeProviderMock.Object.Now().AddDays(30), expiryDate);
        }

        public DateTime ConvertFromSecondsSinceEpoch(int seconds)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(seconds);
        }
    }
}
