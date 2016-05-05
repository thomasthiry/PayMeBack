using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Web.Models;
using PayMeBack.Backend.Contracts.Services;
using AutoMapper;

namespace PayMeBack.Backend.Web.Controllers
{
    public class SplitController : Controller
    {
        private IMapper _mapper;
        private ISplitService _splitService;

        public SplitController(IMapper mapper, ISplitService splitService)
        {
            _mapper = mapper;
            _splitService = splitService;
        }

        // GET: splits
        [HttpGet]
        public IEnumerable<SplitDto> List()
        {
            return _mapper.Map<IEnumerable<SplitDto>>(_splitService.List());
        }

        // GET splits/5
        [HttpGet]
        public SplitDto Get([FromRoute]int id)
        {
            return _mapper.Map<SplitDto>(_splitService.Get(id));
        }

        // POST splits
        [HttpPost]
        public SplitDto Create([FromBody]SplitCreationDto splitCreationDto)
        {
            var createdSplit = _splitService.Create(splitCreationDto.Name, splitCreationDto.Created);
            return _mapper.Map<SplitDto>(createdSplit);
        }

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
