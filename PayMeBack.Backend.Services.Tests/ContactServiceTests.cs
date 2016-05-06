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
        private Mock<IGenericRepository<SplitContact>> _splitContactRepositoryMock;

        public ContactServiceTests()
        {
            _contactRepositoryMock = new Mock<IGenericRepository<Contact>>();
            _splitContactRepositoryMock = new Mock<IGenericRepository<SplitContact>>();
            _contactService = new ContactService(_contactRepositoryMock.Object, _splitContactRepositoryMock.Object);
        }

        [Fact]
        public void ListBySplitId_ReturnsContacts()
        {
            var splitContactsStub = new List<SplitContact> { new SplitContact { ContactId = 1, SplitId = 1 }, new SplitContact { ContactId = 2, SplitId = 1 } };
            _splitContactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<SplitContact, bool>>>())).Returns(splitContactsStub);

            var contactsStub = new List<Contact> { new Contact { Id = 1, Name = "Olivier" }, new Contact { Id = 2, Name = "John" } };
            _contactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<Contact, bool>>>())).Returns(contactsStub);

            var contacts = (List<Contact>)_contactService.ListBySplitId(1);

            Assert.InRange(contacts.Count, 2, 2);
        }

        [Fact]
        public void CreateIfNeededAndAddToSplit_ContactDoesNotExist_CreatesAndReturnsContact()
        {
            _contactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<Contact, bool>>>())).Returns(new List<Contact>());

            var contactStub = new Contact { Id = 5, Email = "john@smith.com" };
            _contactRepositoryMock.Setup(r => r.Insert(It.IsAny<Contact>())).Returns(contactStub);

            var splitContactStub = new SplitContact { ContactId = 5, SplitId = 1 };
            _splitContactRepositoryMock.Setup(r => r.Insert(It.IsAny<SplitContact>())).Returns(splitContactStub);

            var contact = _contactService.CreateIfNeededAndAddToSplit(splitContactStub.SplitId, contactStub.Email);

            Assert.Equal(contactStub.Email, contact.Email);
        }

        [Fact]
        public void CreateIfNeededAndAddToSplit_ContactExists_LinksAndReturnsContact()
        {
            var contactStub = new Contact { Id = 5, Email = "john@smith.com" };
            _contactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<Contact, bool>>>())).Returns(new List<Contact> { contactStub });

            _contactRepositoryMock.Setup(r => r.Insert(It.IsAny<Contact>())).Throws(new Exception("Should not have been called"));

            var splitContactStub = new SplitContact { ContactId = 5, SplitId = 1 };
            _splitContactRepositoryMock.Setup(r => r.Insert(It.IsAny<SplitContact>())).Returns(splitContactStub);

            var contact = _contactService.CreateIfNeededAndAddToSplit(splitContactStub.SplitId, contactStub.Email);

            Assert.Equal(contactStub.Email, contact.Email);
        }
    }
}
