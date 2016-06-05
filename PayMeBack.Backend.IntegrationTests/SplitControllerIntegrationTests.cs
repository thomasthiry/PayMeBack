using AutoMapper;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using PayMeBack.Backend.Repository;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Web.Controllers;
using System;
using System.Linq;
using Xunit;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Web.Configurations;
using PayMeBack.Backend.Web.Models;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNet.Http.Internal;

namespace PayMeBack.Backend.IntegrationTests
{
    public class SplitControllerIntegrationTests
    {
        [Fact]
        public void SplitsList()
        {
            var splitController = CreateController();

            var splitDtos = splitController.Controller.List();

            Assert.InRange(splitDtos.Count(), 1, int.MaxValue);
        }

        [Fact]
        public void SplitsList_ReturnsOnlyTheSplitsOfTheUser()
        {
            var splitController = CreateController();

            var splitDtos = splitController.Controller.List();

            Assert.DoesNotContain(splitDtos, s => s.Name == "Split Of Another User");
        }

        [Fact]
        public void SplitGet()
        {
            var splitController = CreateController();

            var splitDto = splitController.Controller.Get(2);

            Assert.Equal(splitDto.Id, 2);
        }

        [Fact]
        public void SplitCreate()
        {
            var splitController = CreateController();
            var nbSplitsInitial = splitController.Context.Splits.Count();

            var splitCreationDto = new SplitCreationDto { Name = "New Split", Created = new DateTime(2015, 11, 29, 15, 34, 51) };
            splitController.Controller.Create(splitCreationDto);

            var nbSplitsFinal = splitController.Context.Splits.Count();
            Assert.Equal(nbSplitsInitial + 1, nbSplitsFinal);
        }

        [Fact]
        public void SplitSettle()
        {
            var splitController = CreateController();

            var settlementDto = splitController.Controller.Settle(1);

            Assert.InRange(settlementDto.Transfers.Count, 1, int.MaxValue);
        }

        private ControllerWithContext<SplitController> CreateController()
        {
            var services = new ServiceCollection();
            services
                .AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<PayMeBackContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase());

            InjectionConfig.ConfigureCustomServices(services);

            var serviceProvider = services.BuildServiceProvider();
            var context = CreateContext(serviceProvider);

            services.AddTransient<IGenericRepository<Split>, GenericRepository<Split>>(provider => new GenericRepository<Split>(context));

            var mapper = serviceProvider.GetService<IMapper>();
            var splitService = serviceProvider.GetService<ISplitService>();

            var splitController = new SplitController(mapper, splitService);

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") };
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            splitController.ActionContext.HttpContext = httpContext;

            return new ControllerWithContext<SplitController> { Controller = splitController, Context = context };
        }
        private PayMeBackContext CreateContext(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<PayMeBackContext>();

            context.AppUsers.Add(new AppUser { Id = 1, Email = "mark@gmail.com" });
            context.AppUsers.Add(new AppUser { Id = 2, Email = "john@gmail.com" });
            
            context.Splits.Add(new Split { Name = "Today", UserId = 1 });
            context.Splits.Add(new Split { Name = "Yesterday", UserId = 1 });
            context.Splits.Add(new Split { Name = "Split Of Another User", UserId = 2 });

            context.Contacts.Add(new Contact { Name = "John" });
            context.Contacts.Add(new Contact { Name = "Mark" });

            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 1, Owes = 50m });
            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 2, Paid = 50m });
            context.SaveChanges();

            return context;
        }
    }
}
