using Moq;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using System;
using Xunit;

namespace PayMeBack.Backend.Services.Tests
{
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void Create_CallsRepoWithHashAndSaltAndReturnsCreatedUser()
        {
            var password = "MyPass1";
            var userToCreate = new AppUser { Name = "John Smith", Email = "john@gmail.com" };

            _userRepositoryMock.Setup(r => r.Create(It.Is<AppUser>(
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
    }
}
