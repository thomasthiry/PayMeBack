using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Web.Models;
using PayMeBack.Backend.Contracts.Services;
using AutoMapper;
using System;

namespace PayMeBack.Backend.Web.Controllers
{
    public class ContactController : Controller
    {
        private IMapper _mapper;
        private IContactService _contactService;

        public ContactController(IMapper mapper, IContactService contactService)
        {
            _mapper = mapper;
            _contactService = contactService;
        }

        [HttpGet]
        public IEnumerable<SplitContactDto> ListSplitContactsBySplit([FromRoute]int splitId)
        {
            return _mapper.Map<IEnumerable<SplitContactDto>>(_contactService.ListSplitContactsBySplitId(splitId));
        }

        [HttpPost]
        public ContactDto CreateIfNeededAndAddToSplit([FromRoute]int splitId, [FromBody]ContactCreationDto contactCreationDto)
        {
            return _mapper.Map<ContactDto>(_contactService.CreateIfNeededAndAddToSplit(splitId, contactCreationDto.Email, contactCreationDto.Name));
        }

        [HttpGet]
        public SplitContactDto GetSplitContact(int splitId, int splitContactId)
        {
            var splitContact = _contactService.GetSplitContactById(splitContactId);

            var contact = _contactService.GetContactById(splitContact.ContactId);

            return new SplitContactDto { // TODO MAP WITH AUTOMAPPER
                Email = contact.Email,
                Name = contact.Name,
                Owes = splitContact.Owes,
                Paid = splitContact.Paid,
                Iban = splitContact.Contact.Iban,
                Address = splitContact.Contact.Address,
                Comments = splitContact.Comments
            };
        }

        public void UpdateSplitContact(int splitId, int splitContactId, [FromBody]SplitContactUpdateDto splitContactUpdateDto)
        {
            _contactService.UpdateSplitContact(splitContactId, splitContactUpdateDto.Owes, splitContactUpdateDto.Paid, splitContactUpdateDto.Iban, splitContactUpdateDto.Address, splitContactUpdateDto.Comments);
        }
    }
}
