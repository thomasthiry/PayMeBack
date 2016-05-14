using Xunit;

namespace PayMeBack.Backend.Models.Tests
{
    public class SplitContactTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(50, 0, -50)]
        [InlineData(0, 50, 50)]
        [InlineData(50, 50, 0)]
        [InlineData(25, 5, -20)]
        [InlineData(5, 25, 20)]
        public void PaidBalance(decimal owes, decimal paid, decimal expectedPaidBalance)
        {
            var splitContact = new SplitContact { Owes = owes, Paid = paid };

            Assert.Equal(expectedPaidBalance, splitContact.PaidBalance);
        }
    }
}
