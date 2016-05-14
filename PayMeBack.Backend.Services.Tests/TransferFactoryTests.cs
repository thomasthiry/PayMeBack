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
    public class TransferFactoryTests
    {
        [Theory]
        [MemberData(nameof(GetCanStillCreateTransferTestCases))]
        public void CanStillCreateTransfer(IList<SplitContact> splitContacts, bool expectedReturn, string assertFailMessage)
        {
            var transferFactory = new TransferFactory(splitContacts);

            Assert.True(transferFactory.CanStillCreateTransfer() == expectedReturn, assertFailMessage);
        }

        public static IEnumerable<object[]> GetCanStillCreateTransferTestCases()
        {
            yield return new object[] { new List<SplitContact>(), false, "Empty split contact list should return false" };
            yield return new object[] { new List<SplitContact> { new SplitContact { Contact = new Contact { Id = 1 }, Owes = 25m } }, false, "Only an ower should return false" };
            yield return new object[] { new List<SplitContact> { new SplitContact { Contact = new Contact { Id = 1 }, Paid = 25m } }, false, "Only a payer should return false" };
            yield return new object[] { new List<SplitContact>
            {
                new SplitContact { Contact = new Contact { Id = 1 }, Paid = 0m },
                new SplitContact { Contact = new Contact { Id = 2 }, Owes = 0m }
            }, false, "Two neutral should return false" };
            yield return new object[] { new List<SplitContact>
            {
                new SplitContact { Contact = new Contact { Id = 1 }, Paid = 25m },
                new SplitContact { Contact = new Contact { Id = 2 }, Owes = 25m }
            }, true, "One ower and one payer should return true" };
            yield return new object[] { new List<SplitContact>
            {
                new SplitContact { Contact = new Contact { Id = 1 }, Paid = 25m },
                new SplitContact { Contact = new Contact { Id = 2 }, Owes = 0m }
            }, false, "One payer and one neutral should return false" };
            yield return new object[] { new List<SplitContact>
            {
                new SplitContact { Contact = new Contact { Id = 1 }, Paid = 0m },
                new SplitContact { Contact = new Contact { Id = 2 }, Owes = 25m }
            }, false, "One ower and one neutral should return false" };
        }
    }
}
