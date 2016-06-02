using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using PayMeBack.Backend.Repository;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Web.Controllers;
using System;
using Xunit;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Web.Configurations;
using PayMeBack.Backend.Web.Models;
using AutoMapper;
using JWT;
using JWT.DNX.Json.Net;
using Moq;

namespace PayMeBack.Backend.IntegrationTests
{
    public class UserControllerIntegrationTests
    {
        public UserControllerIntegrationTests()
        {
            JsonWebToken.JsonSerializer = new JsonNetJWTSerializer();
        }

        [Fact]
        public void UserCreate()
        {
            var controller = CreateController();

            var createdUserDto = controller.Controller.UserCreate(new UserCreationRequestDto { Email = "john@gmail.com", Name = "John Smith", Password = "MyPass1" });

            Assert.Equal(2, createdUserDto.Id);
        }

        [Fact]
        public void UserLogin()
        {
            var controller = CreateController();

            var loginRequestDto = new LoginRequestDto { Email = "john@gmail.com", Password = "MyPass1" };

            var userAndTokenDto = controller.Controller.Login(loginRequestDto);

            Assert.Equal(loginRequestDto.Email, userAndTokenDto.User.Email);
        }

        private ControllerWithContext<UserController> CreateController()
        {
            var services = new ServiceCollection();
            services
                .AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<PayMeBackContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase());

            InjectionConfig.ConfigureCustomServices(services);

            var serviceProvider = services.BuildServiceProvider();
            var context = CreateContext(serviceProvider);

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(d => d.Now()).Returns(new DateTime(2020, 6, 1, 12, 0, 0, DateTimeKind.Utc));
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>(provider => (DateTimeProvider)dateTimeProviderMock.Object);

            services.AddTransient<IGenericRepository<AppUser>, GenericRepository<AppUser>>(provider => new GenericRepository<AppUser>(context));

            var mapper = serviceProvider.GetService<IMapper>();
            var userService = serviceProvider.GetService<IUserService>();

            var userController = new UserController(mapper, userService);

            return new ControllerWithContext<UserController> { Controller = userController, Context = context };
        }

        private PayMeBackContext CreateContext(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<PayMeBackContext>();

            context.AppUsers.Add(new AppUser { Email = "john@gmail.com", Name = "John Smith", PasswordHash = "YkxknXOgltAnhEhO6yjm6EOwIVMO7NIF02x1Zcj99kA=", PasswordSalt = "JjsV9YytLSuZcQIsrIk6cg==" });
            context.SaveChanges();

            return context;
        }
    }
}
