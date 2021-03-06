﻿using Moq;
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
        public void ListSplitContactsBySplit_ReturnsContactDtos()
        {
            var splitContactStubs = new List<SplitContact>
            {
                new SplitContact { Id = 1, Contact = new Contact { Email = "john@msn.com" } },
                new SplitContact { Id = 2, Contact = new Contact { Email = "mark@msn.com" } },
            };
            _contactServiceMock.Setup(s => s.ListSplitContactsBySplitId(1)).Returns(splitContactStubs);

            var splitContacts = _controller.ListSplitContactsBySplit(1);

            Assert.Equal(splitContactStubs.First().Id, splitContacts.First().Id);
            Assert.Equal(splitContactStubs.First().Contact.Email, splitContacts.First().Email);
            Assert.IsAssignableFrom<SplitContactDto>(splitContacts.First());
        }

        [Fact]
        public void CreateIfNeededAndAddToSplit_ReturnsNewContactDto()
        {
            var contactStub = new Contact { Email = "john@smith.com", Name = "John Smith" };
            _contactServiceMock.Setup(s => s.CreateIfNeededAndAddToSplit(1, contactStub.Email, contactStub.Name)).Returns(contactStub);

            var contact = _controller.CreateIfNeededAndAddToSplit(1, new ContactCreationDto { Email = contactStub.Email, Name = contactStub.Name });

            Assert.Equal(contactStub.Email, contact.Email);
            Assert.Equal(contactStub.Name, contact.Name);
        }

        [Fact]
        public void GetSplitContact_ReturnsSplitContactDto()
        {
            var splitContactStub = new SplitContact { Id = 1, ContactId = 2, Contact = new Contact { Iban = "BE68 5390 0754 7034", Address = "Rue des longiers, 45" }, SplitId = 5, Owes = 25m, Paid = 50m, Comments = "me devait déjà 5 euros" };
            _contactServiceMock.Setup(s => s.GetSplitContactById(splitContactStub.Id)).Returns(splitContactStub);

            var contactStub = new Contact { Id = 2, Email = "olivier@test.com" };
            _contactServiceMock.Setup(s => s.GetContactById(contactStub.Id)).Returns(contactStub);

            var splitContactDto = _controller.GetSplitContact(splitContactStub.SplitId, splitContactStub.Id);

            Assert.Equal(contactStub.Email, splitContactDto.Email);
            Assert.Equal(splitContactStub.Owes, splitContactDto.Owes);
            Assert.Equal(splitContactStub.Paid, splitContactDto.Paid);
            Assert.Equal(splitContactStub.Contact.Iban, splitContactDto.Iban);
            Assert.Equal(splitContactStub.Contact.Address, splitContactDto.Address);
            Assert.Equal(splitContactStub.Comments, splitContactDto.Comments);
        }

        [Fact]
        public void UpdateSplitContact_ReturnsOk()
        {
            var splitId = 1;
            var splitContactId = 2;
            var splitContactUpdateDto = new SplitContactUpdateDto { Owes = 25m, Paid = 50m, Iban = "BE68 5390 0754 7034", Address = "Rue des longiers, 45", Comments = "me devait déjà 5 euros" };

            _controller.UpdateSplitContact(splitId, splitContactId, splitContactUpdateDto);

            _contactServiceMock.Verify(s => s.UpdateSplitContact(splitContactId, splitContactUpdateDto.Owes, splitContactUpdateDto.Paid, splitContactUpdateDto.Iban, splitContactUpdateDto.Address, splitContactUpdateDto.Comments), Times.Once());
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
