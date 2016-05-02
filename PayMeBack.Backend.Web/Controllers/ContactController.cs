using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Web.Models;
using PayMeBack.Backend.Contracts.Services;
using AutoMapper;

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
        public IEnumerable<ContactDto> ListBySplit(int splitId)
        {
            return _mapper.Map<IEnumerable<ContactDto>>(_contactService.ListBySplitId(splitId));
        }

        //// GET api/splits/5
        //[HttpGet]
        //public SplitDto Get(int id)
        //{
        //    return null;
        //}

        //// POST api/splits
        //[HttpPost]
        //public SplitDto Post([FromBody]SplitCreationDto splitCreationDto)
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
