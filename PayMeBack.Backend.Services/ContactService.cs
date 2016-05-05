using System.Collections.Generic;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;
using System;

namespace PayMeBack.Backend.Services
{
    public class ContactService : IContactService
    {
        private IGenericRepository<Contact> _contactRepository;

        public ContactService(IGenericRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<Contact> ListBySplitId(int splitId)
        {
            return _contactRepository.Get(s => s.SplitId == splitId);
        }

        public Contact CreateIfNeededAndAddToSplit(int splitId, string email)
        {
            var contact = new Contact { Name = email, Email = email, SplitId = splitId };
            return _contactRepository.Insert(contact);
        }
    }
}
