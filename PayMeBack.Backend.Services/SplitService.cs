using System;
using System.Collections.Generic;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;
using System.Linq;

namespace PayMeBack.Backend.Services
{
    public class SplitService : ISplitService
    {
        private IGenericRepository<Split> _splitRepository;
        private IGenericRepository<Contact> _contactRepository;
        private IGenericRepository<SplitContact> _splitContactRepository;

        public SplitService(IGenericRepository<Split> splitRepository, IGenericRepository<Contact> contactRepository, IGenericRepository<SplitContact> splitContactRepository)
        {
            _splitRepository = splitRepository;
            _contactRepository = contactRepository;
            _splitContactRepository = splitContactRepository;
        }

        public Split Get(int id)
        {
            return _splitRepository.GetById(id);
        }

        public IEnumerable<Split> List()
        {
            return _splitRepository.Get();
        }

        public Split Create(string name, DateTime created)
        {
            var split = new Split { Name = name, Created = created };
            return _splitRepository.Insert(split);
        }

        public Settlement Settle(int splitId)
        {
            var settlement = new Settlement { Transfers = new List<SettlementTransfer>() };
            var splitContacts = (List<SplitContact>)_splitContactRepository.Get(sc => sc.Split.Id == splitId);

            if (splitContacts.Count == 1)
            {
                return settlement;
            }

            if (splitContacts.Count > 1)
            {
                var transferFactory = new TransferFactory(splitContacts);

                while (transferFactory.CanStillCreateTransfer())
                {
                    var transfer = transferFactory.CreateTransfer();
                    settlement.Transfers.Add(transfer);
                }
            }

            return settlement;
        }
    }
}
