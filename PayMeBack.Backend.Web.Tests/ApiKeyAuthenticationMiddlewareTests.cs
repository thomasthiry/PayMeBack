using PayMeBack.Backend.Web.Middleware;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using PayMeBack.Backend.Services;
using Moq;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace PayMeBack.Backend.Web.Tests
{
    public class ApiKeyAuthenticationMiddlewareTests
    {
        private Mock<IUserService> _userServiceMock;
        private HttpContext _context;
        private ApiKeyAuthenticationMiddleware _middleware;

        public ApiKeyAuthenticationMiddlewareTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _context = new DefaultHttpContext();
            _middleware = new ApiKeyAuthenticationMiddleware(new RequestDelegate(c => Task.CompletedTask), _userServiceMock.Object);
        }

        [Fact]
        public async Task Authentication_NoAuthorizationHeader_ShouldReturn401()
        {
            await _middleware.Invoke(_context);

            Assert.Equal(401, _context.Response.StatusCode);
        }

        [Fact]
        public async Task Authentication_AuthorizationHeaderDoesNotStartWithBearer_ShouldReturn401()
        {
            _context.Request.Headers.Add("Authorization", "Does not start with bearer");

            await _middleware.Invoke(_context);

            Assert.Equal(401, _context.Response.StatusCode);
        }

        [Fact]
        public async Task Authentication_AuthorizationHeaderIsProvided_ServiceIsCalledWithToken()
        {
            var userStub = new AppUser { Id = 5, Email = "thomas@gmail.com" };
            _userServiceMock.Setup(s => s.GetUserForToken(It.Is<string>(t => t == "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjN9.tWz1EhWgVV-LCG1-uUBYt_K11tefk45bfC3K5pVYfpo"))).Returns(userStub);

            _context.Request.Headers.Add("Authorization", "Bearer: eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjN9.tWz1EhWgVV-LCG1-uUBYt_K11tefk45bfC3K5pVYfpo");

            await _middleware.Invoke(_context);

            _userServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Authentication_AuthorizationHeaderIsProvidedAndServiceValidatesIt_ShouldProceed()
        {
            var userStub = new AppUser { Id = 5, Email = "thomas@gmail.com" };
            _userServiceMock.Setup(s => s.GetUserForToken(It.Is<string>(t => t == "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjN9.tWz1EhWgVV-LCG1-uUBYt_K11tefk45bfC3K5pVYfpo"))).Returns(userStub);

            _context.Request.Headers.Add("Authorization", "Bearer: eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjN9.tWz1EhWgVV-LCG1-uUBYt_K11tefk45bfC3K5pVYfpo");

            await _middleware.Invoke(_context);

            Assert.Equal(200, _context.Response.StatusCode);
        }

        [Fact]
        public async Task Authentication_AuthorizationHeaderIsProvidedAndServiceValidatesIt_IdentityShouldBeSetInContext()
        {
            var userStub = new AppUser { Id = 5, Email = "thomas@gmail.com" };
            _userServiceMock.Setup(s => s.GetUserForToken(It.Is<string>(t => t == "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjN9.tWz1EhWgVV-LCG1-uUBYt_K11tefk45bfC3K5pVYfpo"))).Returns(userStub);

            _context.Request.Headers.Add("Authorization", "Bearer: eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjN9.tWz1EhWgVV-LCG1-uUBYt_K11tefk45bfC3K5pVYfpo");

            await _middleware.Invoke(_context);

            Assert.Equal(userStub.Id.ToString(), _context.User.GetUserId());
        }
    }
}
