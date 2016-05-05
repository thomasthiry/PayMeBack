using Moq;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace PayMeBack.Backend.Services.Tests
{
    public class ContactServiceTests
    {
        private IContactService _contactService;
        private Mock<IGenericRepository<Contact>> _contactRepositoryMock;

        public ContactServiceTests()
        {
            _contactRepositoryMock = new Mock<IGenericRepository<Contact>>();
            _contactService = new ContactService(_contactRepositoryMock.Object);
        }

        [Fact]
        public void ListBySplitId_ReturnsContacts()
        {
            var contactsStub = new List<Contact>
            {
                new Contact { Name = "Olivier", SplitId = 1 },
                new Contact { Name = "John", SplitId = 1 },
            };
            _contactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<Contact, bool>>>())).Returns(contactsStub);

            var contacts = _contactService.ListBySplitId(1);

            Assert.NotEmpty(contacts);
        }

        [Fact]
        public void CreateIfNeededAndAddToSplit_ReturnsCreatedContact()
        {
            var contactStub = new Contact { Email = "john@smith.com", SplitId = 1 };
            _contactRepositoryMock.Setup(r => r.Insert(It.IsAny<Contact>())).Returns(contactStub);

            var contact = _contactService.CreateIfNeededAndAddToSplit(contactStub.SplitId, contactStub.Email);

            Assert.Equal(contactStub.Email, contact.Email);
        }
    }
}
