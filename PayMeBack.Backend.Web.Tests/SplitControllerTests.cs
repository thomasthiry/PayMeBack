using Microsoft.AspNet.Http.Internal;
using Moq;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Web.Configurations;
using PayMeBack.Backend.Web.Controllers;
using PayMeBack.Backend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace PayMeBack.Backend.Web.Tests
{
    public class SplitControllerTests
    {
        private SplitController _controller;
        private Mock<ISplitService> _splitServiceMock;

        public SplitControllerTests()
        {
            _splitServiceMock = new Mock<ISplitService>();

            var mapper = MapperConfig.CreateMapper();
            _controller = new SplitController(mapper, _splitServiceMock.Object);

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") };
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            _controller.ActionContext.HttpContext = httpContext;
        }

        [Fact]
        public void GetSplits_ReturnsSplitDtos()
        {
            var splitsStub = new List<Split>
            {
                new Split { Name = "Tomorrow" },
                new Split { Name = "Yesterday" },
            };
            _splitServiceMock.Setup(s => s.List(1)).Returns(splitsStub);

            var splits = _controller.List();

            Assert.NotEmpty(splits);
            Assert.IsAssignableFrom<SplitDto>(splits.First());
        }

        [Fact]
        public void GetSplit_GetOneById_ReturnsSplit()
        {
            var splitStub = new Split { Id = 1, Name = "Tomorrow" };
            _splitServiceMock.Setup(s => s.Get(1)).Returns(splitStub);

            var split = _controller.Get(1);

            Assert.Equal(splitStub.Name, split.Name);
        }

        [Fact]
        public void CreateSplit_ReturnsTheCreatedSplit()
        {
            var splitCreationDto = new SplitCreationDto { Name = "Created Split", Created = new DateTime(2016, 12, 05, 12, 30, 58) };

            var splitStub = new Split { Id = 3, Name = splitCreationDto.Name, Created = splitCreationDto.Created };
            _splitServiceMock.Setup(s => s.Create(It.Is<string>(n => n == splitCreationDto.Name), It.Is<DateTime>(c => c == splitCreationDto.Created))).Returns(splitStub);

            var splitDto = _controller.Create(splitCreationDto);

            Assert.Equal(3, splitDto.Id);

            Assert.Equal(splitCreationDto.Name, splitDto.Name);
            Assert.Equal(splitCreationDto.Created, splitDto.Created);
        }

        [Fact]
        public void Settle_ReturnsSettlement()
        {
            var settlementStub = new Settlement { Split = new Split { Id = 1 }, Transfers = new List<SettlementTransfer> { new SettlementTransfer { FromContact = new Contact { Name = "John" }, ToContact = new Contact { Name = "Mark" }, Amount = 25m } } };
            _splitServiceMock.Setup(s => s.Settle(settlementStub.Split.Id)).Returns(settlementStub);

            var settlementDto = _controller.Settle(settlementStub.Split.Id);

            Assert.Equal(1, settlementDto.Transfers.Count);
            Assert.Equal(settlementStub.Transfers[0].Amount, settlementDto.Transfers[0].Amount);
            Assert.Equal(settlementStub.Split.Id, settlementDto.Split.Id);
        }
    }
}
