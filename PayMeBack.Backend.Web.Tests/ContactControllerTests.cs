using Moq;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Web.Configurations;
using PayMeBack.Backend.Web.Controllers;
using PayMeBack.Backend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PayMeBack.Backend.Web.Tests
{
    public class ContactControllerTests
    {
        private ContactController _controller;
        private Mock<IContactService> _contactServiceMock;

        public ContactControllerTests()
        {
            _contactServiceMock = new Mock<IContactService>();

            var mapper = MapperConfig.CreateMapper();
            _controller = new ContactController(mapper, _contactServiceMock.Object);
        }

        [Fact]
        public void GetContacts_ReturnsContactDtos()
        {
            var contactsStub = new List<Contact>
            {
                new Contact { Email = "Olivier" },
                new Contact { Email = "John" },
            };
            _contactServiceMock.Setup(s => s.ListBySplitId(1)).Returns(contactsStub);

            var contacts = _controller.ListBySplit(1);

            Assert.NotEmpty(contacts);
            Assert.IsAssignableFrom<ContactDto>(contacts.First());
        }

        [Fact]
        public void CreateIfNeededAndAddToSplit_ReturnsNewContactDto()
        {
            var contactStub = new Contact { Email = "Olivier" };
            _contactServiceMock.Setup(s => s.CreateIfNeededAndAddToSplit(1, contactStub.Email)).Returns(contactStub);

            var contact = _controller.CreateIfNeededAndAddToSplit(1, new ContactCreationDto { Email = contactStub.Email });

            Assert.Equal(contactStub.Email, contact.Email);
        }

        [Fact]
        public void GetSplitContact_ReturnsSplitContactDto()
        {
            var splitContactStub = new SplitContact { Id = 1, ContactId = 2, SplitId = 5 };
            _contactServiceMock.Setup(s => s.GetSplitContactById(splitContactStub.Id)).Returns(splitContactStub);

            var contactStub = new Contact { Id = 2, Email = "olivier@test.com" };
            _contactServiceMock.Setup(s => s.GetContactById(contactStub.Id)).Returns(contactStub);

            var splitContactDto = _controller.GetSplitContact(splitContactStub.SplitId, splitContactStub.Id);

            Assert.Equal(contactStub.Email, splitContactDto.Email);
        }

        //[Fact]
        //public void GetSplit_GetOneById_ReturnsSplit()
        //{
        //    var splitStub = new Split { Id = 1, Name = "Tomorrow" };
        //    _splitServiceMock.Setup(s => s.Get(1)).Returns(splitStub);

        //    var split = _controller.Get(1);

        //    Assert.Equal(splitStub.Name, split.Name);
        //}

        //[Fact]
        //public void CreateSplit_ReturnsTheCreatedSplit()
        //{
        //    var splitCreationDto = new SplitCreationDto { Name = "Created Split", Created = new DateTime(2016, 12, 05, 12, 30, 58) };

        //    var splitStub = new Split { Id = 3, Name = splitCreationDto.Name, Created = splitCreationDto.Created };
        //    _splitServiceMock.Setup(s => s.Create(It.Is<string>(n => n == splitCreationDto.Name), It.Is<DateTime>(c => c == splitCreationDto.Created))).Returns(splitStub);

        //    var splitDto = _controller.Post(splitCreationDto);

        //    Assert.Equal(3, splitDto.Id);

        //    Assert.Equal(splitCreationDto.Name, splitDto.Name);
        //    Assert.Equal(splitCreationDto.Created, splitDto.Created);
        //}
    }
}
