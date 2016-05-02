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
        public void GetContacts_ReturnsContactDtos()
        {
            var contactsStub = new List<Contact>
            {
                new Contact { Name = "Olivier" },
                new Contact { Name = "John" },
            };
            _contactServiceMock.Setup(r => r.ListBySplitId(1)).Returns(contactsStub);

            var contacts = _controller.ListBySplit(1);

            Assert.NotEmpty(contacts);
            Assert.IsAssignableFrom<ContactDto>(contacts.First());
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
