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

        public IEnumerable<SplitContact> ListSplitContactsBySplitId(int splitId)
        {
            var splitContacts = _splitContactRepository.GetWithIncludedProperties(sc => sc.Contact, sc => sc.SplitId == splitId);

            return splitContacts;
        }

        public Contact CreateIfNeededAndAddToSplit(int splitId, string email, string name)
        {
            var contact = _contactRepository.Get(c => c.Email == email).FirstOrDefault();

            if (contact == null)
            {
                var nameToUse = string.IsNullOrEmpty(name) ? email : name;
                contact = new Contact { Name = nameToUse, Email = email };
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

        public void UpdateSplitContact(int splitContactId, decimal owes, decimal paid, string iban, string address, string comments)
        {
            var splitContact = _splitContactRepository.GetWithIncludedProperties(sc => sc.Contact, sc => sc.Id == splitContactId).FirstOrDefault();

            splitContact.Owes = owes;
            splitContact.Paid = paid;
            splitContact.Contact.Iban = iban;
            splitContact.Contact.Address = address;
            splitContact.Comments = comments;

            _splitContactRepository.Update(splitContact);
        }
    }
}
