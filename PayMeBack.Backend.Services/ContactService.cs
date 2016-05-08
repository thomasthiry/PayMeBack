using System.Collections.Generic;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;
using System;
using System.Linq;

namespace PayMeBack.Backend.Services
{
    public class ContactService : IContactService
    {
        private IGenericRepository<Contact> _contactRepository;
        private IGenericRepository<SplitContact> _splitContactRepository;

        public ContactService(IGenericRepository<Contact> contactRepository, IGenericRepository<SplitContact> splitContactRepository)
        {
            _contactRepository = contactRepository;
            _splitContactRepository = splitContactRepository;
        }

        public IEnumerable<Contact> ListBySplitId(int splitId)
        {
            var splitContacts = _splitContactRepository.Get(sc => sc.SplitId == splitId);
            var contactsIds = splitContacts.Select(sc => sc.ContactId);

            return _contactRepository.Get(c => contactsIds.Contains(c.Id));
        }

        public Contact CreateIfNeededAndAddToSplit(int splitId, string email)
        {
            var contact = _contactRepository.Get(c => c.Email == email).FirstOrDefault();

            if (contact == null)
            {
                contact = new Contact { Name = email, Email = email };
                _contactRepository.Insert(contact);
            }

            _splitContactRepository.Insert(new SplitContact { SplitId = splitId, ContactId = contact.Id });
            
            return contact;
        }

        public Contact GetContactById(int contactId)
        {
            return _contactRepository.Get(c => c.Id == contactId).FirstOrDefault();
        }

        public SplitContact GetSplitContactById(int splitContactId)
        {
            return _splitContactRepository.Get(sc => sc.Id == splitContactId).FirstOrDefault();
        }

        public void UpdateSplitContact(int splitContactId, decimal owes, decimal paid, string comments)
        {
            var splitContact = _splitContactRepository.Get(sc => sc.Id == splitContactId).FirstOrDefault();

            splitContact.Owes = owes;
            splitContact.Paid = paid;
            splitContact.Comments = comments;

            _splitContactRepository.Update(splitContact);
        }
    }
}
