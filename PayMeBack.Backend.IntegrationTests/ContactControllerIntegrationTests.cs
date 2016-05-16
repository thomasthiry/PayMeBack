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

namespace PayMeBack.Backend.IntegrationTests
{
    public class ContactControllerIntegrationTests
    {
        [Fact]
        public void SplitContactsList()
        {
            var contactController = CreateController();

            var splitContactDtos = contactController.Controller.ListSplitContactsBySplit(1);

            Assert.InRange(splitContactDtos.Count(), 1, int.MaxValue);
        }

        [Fact]
        public void CreateIfNeededAndAddToSplit()
        {
            var contactController = CreateController();

            var contactCreationDto = new ContactCreationDto { Email = "john@smith.com" };
            var contactDto = contactController.Controller.CreateIfNeededAndAddToSplit(1, contactCreationDto);

            Assert.Equal(contactCreationDto.Email, contactDto.Email);
        }

        [Fact]
        public void GetSplitContact()
        {
            var contactController = CreateController();

            var splitContactDto = contactController.Controller.GetSplitContact(1, 1);

            Assert.Equal(50m, splitContactDto.Owes);
        }

        [Fact]
        public void UpdateSplitContact()
        {
            var contactController = CreateController();

            var splitContactUpdateDto = new SplitContactUpdateDto { Owes = 33, Paid = 33, Comments = "comment" };
            var splitContactId = 1;
            contactController.Controller.UpdateSplitContact(1, splitContactId, splitContactUpdateDto);

            Assert.Equal(splitContactUpdateDto.Comments, contactController.Context.SplitContacts.Single(sc => sc.Id == splitContactId).Comments);
        }

        private ControllerWithContext<ContactController> CreateController()
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
            services.AddTransient<IGenericRepository<SplitContact>, GenericRepository<SplitContact>>(provider => new GenericRepository<SplitContact>(context));

            var mapper = serviceProvider.GetService<IMapper>();
            var splitService = serviceProvider.GetService<IContactService>();

            var contactController = new ContactController(mapper, splitService);

            return new ControllerWithContext<ContactController> { Controller = contactController, Context = context };
        }
        private PayMeBackContext CreateContext(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<PayMeBackContext>();

            context.Splits.Add(new Split { Name = "Today" });
            context.Splits.Add(new Split { Name = "Yesterday" });

            context.Contacts.Add(new Contact { Name = "John" });
            context.Contacts.Add(new Contact { Name = "Mark" });

            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 1, Owes = 50m });
            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 2, Paid = 50m });
            context.SaveChanges();

            return context;
        }
    }
}
