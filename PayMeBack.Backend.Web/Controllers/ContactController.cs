﻿using System.Collections.Generic;
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
            return _mapper.Map<ContactDto>(_contactService.CreateIfNeededAndAddToSplit(splitId, contactCreationDto.Email));
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
                Comments = splitContact.Comments
            };
        }

        public void UpdateSplitContact(int splitId, int splitContactId, [FromBody]SplitContactUpdateDto splitContactUpdateDto)
        {
            _contactService.UpdateSplitContact(splitContactId, splitContactUpdateDto.Owes, splitContactUpdateDto.Paid, splitContactUpdateDto.Comments);
        }

        // POST api/splits
        //[HttpPost]
        //public SplitDto CreateIfNeededAndAddToSplit([FromBody]SplitCreationDto splitCreationDto)
        //{
        //    var createdSplit = _splitService.Create(splitCreationDto.Name, splitCreationDto.Created);
        //    return _mapper.Map<SplitDto>(createdSplit);
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //    throw new NotImplementedException();
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
