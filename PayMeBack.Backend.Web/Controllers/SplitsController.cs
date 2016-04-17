using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Web.Models;
using System.Linq;
using PayMeBack.Backend.Contracts.Services;
using AutoMapper;

namespace PayMeBack.Backend.Web.Controllers
{
    [Route("[controller]")]
    public class SplitsController : Controller
    {
        private IList<SplitDto> _splits = new List<SplitDto> { new SplitDto { Id = 1, Name = "Today" }, new SplitDto { Id = 2, Name = "Tomorrow" } };

        private IMapper _mapper;
        private ISplitService _splitService;

        public SplitsController(IMapper mapper, ISplitService splitService)
        {
            _mapper = mapper;
            _splitService = splitService;
        }

        // GET: api/splits
        [HttpGet]
        public IEnumerable<SplitDto> Get()
        {
            return _mapper.Map<IEnumerable<SplitDto>>(_splitService.List());
        }

        // GET api/splits/5
        [HttpGet("{id}")]
        public SplitDto Get(int id)
        {
            return _mapper.Map<SplitDto>(_splitService.Get(id));
        }

        // POST api/splits
        [HttpPost]
        public SplitDto Create([FromBody]SplitCreationDto splitCreationDto)
        {
            return _mapper.Map<SplitDto>(_splitService.Create(splitCreationDto.Name, splitCreationDto.Created));
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
