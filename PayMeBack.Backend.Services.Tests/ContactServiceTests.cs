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
        public void ListSplitContactsBySplitId_ReturnsContacts()
        {
            var splitContactsStub = new List<SplitContact> { new SplitContact { ContactId = 1, SplitId = 1 }, new SplitContact { ContactId = 2, SplitId = 1 } };
            _splitContactRepositoryMock.Setup(r => r.GetWithIncludedProperties(It.IsAny<Expression<Func<SplitContact, IEntity>>>(), It.IsAny<Expression<Func<SplitContact, bool>>>())).Returns(splitContactsStub);

            var contacts = (List<SplitContact>)_contactService.ListSplitContactsBySplitId(1);

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

        [Fact]
        public void GetContactById_ReturnsContact()
        {
            var contactStub = new Contact { Id = 5, Email = "john@smith.com" };
            _contactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<Contact, bool>>>())).Returns(new List<Contact> { contactStub });

            var contact = _contactService.GetContactById(contactStub.Id);

            Assert.Equal(contactStub.Email, contact.Email);
        }

        [Fact]
        public void GetSplitContactById_ReturnsSplitContact()
        {
            var splitContactStub = new SplitContact { Id = 5, ContactId = 30, Owes = 25m, Paid = 50m, Comments = "my comment" };
            _splitContactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<SplitContact, bool>>>())).Returns(new List<SplitContact> { splitContactStub });

            var splitContact = _contactService.GetSplitContactById(splitContactStub.Id);

            Assert.Equal(splitContactStub.ContactId, splitContact.ContactId);
            Assert.Equal(splitContactStub.Owes, splitContact.Owes);
            Assert.Equal(splitContactStub.Paid, splitContact.Paid);
            Assert.Equal(splitContactStub.Comments, splitContact.Comments);
        }

        [Fact]
        public void UpdateSplitContact_CallsRepository()
        {
            var splitContactStub = new SplitContact { Id = 5, ContactId = 30, Contact = new Contact(), Owes = 25m, Paid = 50m, Comments = "my comment" };
            _splitContactRepositoryMock.Setup(r => r.GetWithIncludedProperties(It.IsAny<Expression<Func<SplitContact, IEntity>>>(), It.IsAny<Expression<Func<SplitContact, bool>>>())).Returns(new List<SplitContact> { splitContactStub });
            
            var splitContactWithNewValues = new SplitContact { Id = 5, ContactId = 30, Owes = 99m, Paid = 22m, Contact = new Contact { Iban = "BE68 5390 0754 7034", Address = "Rue des longiers, 45" }, Comments = "updated values" };
            _contactService.UpdateSplitContact(splitContactStub.Id, splitContactWithNewValues.Owes, splitContactWithNewValues.Paid, splitContactWithNewValues.Contact.Iban, splitContactWithNewValues.Contact.Address, splitContactWithNewValues.Comments);

            _splitContactRepositoryMock.Verify(r => r.Update(It.Is<SplitContact>(sc => sc.Owes == splitContactWithNewValues.Owes && sc.Paid == splitContactWithNewValues.Paid && sc.Contact.Iban == splitContactWithNewValues.Contact.Iban && sc.Contact.Address == splitContactWithNewValues.Contact.Address && sc.Comments == splitContactWithNewValues.Comments)));
        }
    }
}
