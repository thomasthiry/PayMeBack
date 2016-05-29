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

namespace PayMeBack.Backend.IntegrationTests
{
    public class UserControllerIntegrationTests
    {
        [Fact]
        public void UserCreate()
        {
            var controller = CreateController();

            var createdUserDto = controller.Controller.UserCreate(new UserCreationRequestDto { Email = "john@gmail.com", Name = "John Smith", Password = "MyPass1" });

            Assert.Equal(2, createdUserDto.Id);
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

            services.AddTransient<IUserRepository, UserRepository>(provider => new UserRepository(context));

            var mapper = serviceProvider.GetService<IMapper>();
            var userService = serviceProvider.GetService<IUserService>();

            var userController = new UserController(mapper, userService);

            return new ControllerWithContext<UserController> { Controller = userController, Context = context };
        }
        private PayMeBackContext CreateContext(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<PayMeBackContext>();

            context.AppUsers.Add(new AppUser { Email = "john@gmail.com", Name = "John Smith" });
            context.SaveChanges();

            return context;
        }
    }
}
