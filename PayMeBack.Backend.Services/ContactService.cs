using System.Collections.Generic;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;

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
    }
}
