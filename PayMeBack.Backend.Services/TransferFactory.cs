using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMeBack.Backend.Services
{
    public class TransferFactory
    {
        private List<SplitContact> _splitContacts;

        private Dictionary<int, decimal> _contactBalances = new Dictionary<int, decimal>();


        public TransferFactory(List<SplitContact> splitContacts)
        {
            _splitContacts = splitContacts;

            _splitContacts.ForEach(sc => _contactBalances.Add(sc.Contact.Id, 0m));
            _splitContacts.ForEach(sc => _contactBalances[sc.Contact.Id] += sc.PaidBalance);
        }

        public bool CanStillCreateTransfer()
        {
            throw new NotImplementedException();
        }

        public SettlementTransfer CreateTransfer()
        {
            var owerBalance = _contactBalances.FirstOrDefault(balance => balance.Value < 0m);
            var owerContact = _splitContacts.Select(sc => sc.Contact).Where(c => c.Id == owerBalance.Key).First();
            var payerBalance = _contactBalances.FirstOrDefault(balance => balance.Value > 0m);
            var payerContact = _splitContacts.Select(sc => sc.Contact).Where(c => c.Id == payerBalance.Key).First();
            var amount = Math.Min(Math.Abs(owerBalance.Value), Math.Abs(payerBalance.Value));

            _contactBalances[owerBalance.Key] += amount;
            _contactBalances[payerBalance.Key] -= amount;

            return new SettlementTransfer { FromContact = owerContact, ToContact = payerContact, Amount = amount };
        }
    }
}
