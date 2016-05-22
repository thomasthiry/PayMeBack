using PayMeBack.Backend.Web.Controllers;
using PayMeBack.Backend.Web.Models;
using Xunit;

namespace PayMeBack.Backend.Web.Tests
{
    public class UserControllerTests
    {
        private UserController _controller;

        public UserControllerTests()
        {
            _controller = new UserController();
        }

        [Fact]
        public void Login_ReturnsToken()
        {
            var tokenDto = _controller.Login(new LoginRequestDto { Username = "thomas@user.com", Password = "Password1" });

            Assert.Equal("IJ9RJZR908JIZ", tokenDto.Token);
        }
    }
}
