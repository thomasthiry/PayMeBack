using Moq;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using System;
using System.Linq.Expressions;
using Xunit;

namespace PayMeBack.Backend.Services.Tests
{
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IGenericRepository<AppUser>> _userRepositoryMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<AppUser>>();
            _userService = new UserService(_userRepositoryMock.Object);
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
    }
}
