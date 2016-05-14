using Moq;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using PayMeBack.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace PayMeBack.Backend.Services.Tests
{
    public class SplitServiceTests
    {
        private ISplitService _splitService;
        private Mock<IGenericRepository<Split>> _splitRepositoryMock;
        private Mock<IGenericRepository<Contact>> _contactRepositoryMock;
        private Mock<IGenericRepository<SplitContact>> _splitContactRepositoryMock;

        public SplitServiceTests()
        {
            _splitRepositoryMock = new Mock<IGenericRepository<Split>>();
            _contactRepositoryMock = new Mock<IGenericRepository<Contact>>();
            _splitContactRepositoryMock = new Mock<IGenericRepository<SplitContact>>();
            _splitService = new SplitService(_splitRepositoryMock.Object, _contactRepositoryMock.Object, _splitContactRepositoryMock.Object);
        }

        [Fact]
        public void Get_ReturnsSplit()
        {
            var splitStub = new Split { Id = 1, Name = "Tomorrow" };

            _splitRepositoryMock.Setup(r => r.GetById(1)).Returns(splitStub);

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
            _splitRepositoryMock.Setup(r => r.Get(null)).Returns(splitsStub);

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

        private static Contact contact1 = new Contact { Id = 1 };
        private static Contact contact2 = new Contact { Id = 2 };
        private static Contact contact3 = new Contact { Id = 3 };

        [Theory]
        [MemberData(nameof(GetSettleTestCases))]
        public void Settle(IList<SplitContact> splitContactsStub, IList<SettlementTransfer> expectedTransfers)
        {
            _splitContactRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<SplitContact, bool>>>())).Returns(splitContactsStub);
            _contactRepositoryMock.Setup(r => r.GetById(1)).Returns(contact1);
            _contactRepositoryMock.Setup(r => r.GetById(2)).Returns(contact2);
            _contactRepositoryMock.Setup(r => r.GetById(3)).Returns(contact3);

            var settlement = _splitService.Settle(1);

            AssertHelper.AssertAreAllFieldsEqualInList(expectedTransfers, settlement.Transfers);
        }

        public static IEnumerable<object[]> GetSettleTestCases()
        {
            yield return new object[] { new List<SplitContact>(), new List<SettlementTransfer>() };
            yield return new object[] { new List<SplitContact> { new SplitContact { Contact = contact1, Owes = 25m } }, new List<SettlementTransfer>() };
            yield return new object[] { new List<SplitContact> { new SplitContact { Contact = contact1, Paid = 25m } }, new List<SettlementTransfer>() };
            yield return new object[] { new List<SplitContact>
            {
                new SplitContact { Contact = contact1, Paid = 25m },
                new SplitContact { Contact = contact2, Owes = 25m }
            }, new List<SettlementTransfer>() { new SettlementTransfer { FromContact = new Contact { Id = 2 }, ToContact = new Contact { Id = 1 }, Amount = 25m } } };
            yield return new object[] { new List<SplitContact>
            {
                new SplitContact { Contact = contact1, Paid = 25m },
                new SplitContact { Contact = contact2, Owes = 10m }
            }, new List<SettlementTransfer>() { new SettlementTransfer { FromContact = new Contact { Id = 2 }, ToContact = new Contact { Id = 1 }, Amount = 10m } } };
            yield return new object[] { new List<SplitContact>
            {
                new SplitContact { Contact = contact1, Owes = 25m },
                new SplitContact { Contact = contact2, Paid = 10m }
            }, new List<SettlementTransfer>() { new SettlementTransfer { FromContact = new Contact { Id = 1 }, ToContact = new Contact { Id = 2 }, Amount = 10m } } };
            //yield return new object[] { new List<SplitContact>
            //{
            //    new SplitContact { Contact = contact1, Owes = 25m },
            //    new SplitContact { Contact = contact2, Paid = 10m },
            //    new SplitContact { Contact = contact3, Paid = 15m }
            //}, new List<SettlementTransfer>() {
            //    new SettlementTransfer { FromContact = new Contact { Id = 1 }, ToContact = new Contact { Id = 2 }, Amount = 10m },
            //    new SettlementTransfer { FromContact = new Contact { Id = 1 }, ToContact = new Contact { Id = 3 }, Amount = 15m }
            //} };
        }
    }
}
