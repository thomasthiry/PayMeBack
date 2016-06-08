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

            var contactCreationDto = new ContactCreationDto { Email = "john@smith.com", Name = "John Smith" };
            var contactDto = contactController.Controller.CreateIfNeededAndAddToSplit(1, contactCreationDto);

            Assert.Equal(contactCreationDto.Email, contactDto.Email);
            Assert.Equal(contactCreationDto.Name, contactDto.Name);
        }

        [Fact]
        public void GetSplitContact()
        {
            var contactController = CreateController();

            var splitContactDto = contactController.Controller.GetSplitContact(1, 1);

            Assert.Equal(50m, splitContactDto.Owes);
            Assert.Equal("BE68 5390 0754 7034", splitContactDto.Iban);
            Assert.Equal("Rue des longiers, 45", splitContactDto.Address);
        }

        [Fact]
        public void UpdateSplitContact()
        {
            var contactController = CreateController();

            var splitContactUpdateDto = new SplitContactUpdateDto { Owes = 33, Paid = 33, Iban = "BE68 5390 0754 7034", Address = "Rue des longiers, 45", Comments = "comment" };
            var splitContactId = 1;
            contactController.Controller.UpdateSplitContact(1, splitContactId, splitContactUpdateDto);

            var splitContact = contactController.Context.SplitContacts.Single(sc => sc.Id == splitContactId);
            Assert.Equal(splitContactUpdateDto.Comments, splitContact.Comments);
            var contact = contactController.Context.Contacts.Single(c => c.Id == splitContact.ContactId);
            Assert.Equal(splitContactUpdateDto.Iban, contact.Iban);
            Assert.Equal(splitContactUpdateDto.Address, contact.Address);
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

            context.Contacts.Add(new Contact { Name = "John", Iban = "BE68 5390 0754 7034", Address = "Rue des longiers, 45" });
            context.Contacts.Add(new Contact { Name = "Mark" });

            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 1, Owes = 50m });
            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 2, Paid = 50m });
            context.SaveChanges();

            return context;
        }
    }
}
