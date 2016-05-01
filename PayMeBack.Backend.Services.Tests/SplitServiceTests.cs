using Moq;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace PayMeBack.Backend.Services.Tests
{
    public class SplitServiceTests
    {
        private ISplitService _splitService;
        private Mock<IGenericRepository<Split>> _splitRepositoryMock;

        public SplitServiceTests()
        {
            _splitRepositoryMock = new Mock<IGenericRepository<Split>>();
            _splitService = new SplitService(_splitRepositoryMock.Object);
        }

        [Fact]
        public void Get_ReturnsSplit()
        {
            var splitStub = new Split { Id = 1, Name = "Tomorrow" };

            _splitRepositoryMock.Setup(r => r.GetByID(1)).Returns(splitStub);

            var split = _splitService.Get(1);

            Assert.Equal(splitStub.Id, split.Id);
        }

        [Fact]
        public void List_ReturnsSplits()
        {
            var splitsStub = new List<Split>
            {
                new Split { Name = "Tomorrow" },
                new Split { Name = "Yesterday" },
            };
            _splitRepositoryMock.Setup(r => r.Get()).Returns(splitsStub);

            var splits = _splitService.List();

            Assert.NotEmpty(splits);
        }

        [Fact]
        public void Create_ReturnsCreatedSplit()
        {
            var splitStub = new Split { Id = 3, Name = "Created split", Created = new DateTime(2016, 12, 05, 12, 30, 58) };
            _splitRepositoryMock.Setup(r => r.Insert(It.Is<Split>(s => s.Name == splitStub.Name && s.Created == splitStub.Created))).Returns(splitStub);

            var split = _splitService.Create(splitStub.Name, splitStub.Created);

            Assert.Equal(splitStub.Name, split.Name);
            Assert.Equal(splitStub.Created, split.Created);
        }
    }
}
