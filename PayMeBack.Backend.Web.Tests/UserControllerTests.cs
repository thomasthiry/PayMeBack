using Moq;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Web.Configurations;
using PayMeBack.Backend.Web.Controllers;
using PayMeBack.Backend.Web.Models;
using System;
using Xunit;

namespace PayMeBack.Backend.Web.Tests
{
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            var mapper = MapperConfig.CreateMapper();
            _controller = new UserController(mapper, _userServiceMock.Object);
        }

        [Fact]
        public void CreateUser_ReturnsCreatedUser()
        {
            var userCreationDto = new UserCreationRequestDto { Email = "john@gmail.com", Name = "John Smith", Password = "MyPass1" };

            var userStub = new AppUser { Id = 3, Email = userCreationDto.Email, Name = userCreationDto.Name };
            _userServiceMock.Setup(s => s.Create(It.Is<string>(e => e == userCreationDto.Email), It.Is<string>(n => n == userCreationDto.Name), It.Is<string>(p => p == userCreationDto.Password))).Returns(userStub);

            var userDto = _controller.UserCreate(userCreationDto);

            Assert.Equal(3, userDto.Id);

            Assert.Equal(userCreationDto.Name, userDto.Name);
            Assert.Equal(userCreationDto.Email, userDto.Email);
        }

        [Fact]
        public void Login_ReturnsUserAndToken()
        {
            var loginRequestDto = new LoginRequestDto { Email = "john@gmail.com", Password = "MyPass1" };

            var userAndTokenStub = new UserAndToken { User = new AppUser { Id = 3, Email = loginRequestDto.Email }, Token = "EZR546EZ4R8ZE4RA8DS5FDSFEZ" };
            _userServiceMock.Setup(s => s.Login(It.Is<string>(e => e == loginRequestDto.Email), It.Is<string>(n => n == loginRequestDto.Password))).Returns(userAndTokenStub);

            var userAndTokenDto = _controller.Login(loginRequestDto);

            Assert.Equal(userAndTokenStub.User.Id, userAndTokenDto.User.Id);
            Assert.Equal(userAndTokenStub.Token, userAndTokenDto.Token);
        }
    }
}
