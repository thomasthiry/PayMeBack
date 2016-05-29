using Moq;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Web.Configurations;
using PayMeBack.Backend.Web.Controllers;
using PayMeBack.Backend.Web.Models;
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

        //[Fact]
        //public void Login_ReturnsToken()
        //{
        //    var tokenDto = _controller.Login(new LoginRequestDto { Username = "thomas@user.com", Password = "Password1" });

        //    Assert.Equal("IJ9RJZR908JIZ", tokenDto.Token);
        //}

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
    }
}
