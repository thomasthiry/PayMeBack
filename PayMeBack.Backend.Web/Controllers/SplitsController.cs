using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Web.Models;
using System.Linq;

namespace PayMeBack.Backend.Web.Controllers
{
    [Route("[controller]")]
    public class SplitsController : Controller
    {
        private IList<SplitDto> _splits = new List<SplitDto> { new SplitDto { Id = 1, Name = "Today" },  new SplitDto { Id = 2, Name = "Tomorrow" } };

        // GET: api/splits
        [HttpGet]
        public IEnumerable<SplitDto> Get()
        {
            return _splits;
        }

        // GET api/splits/5
        [HttpGet("{id}")]
        public SplitDto Get(int id)
        {
            return _splits.SingleOrDefault(s => s.Id == id);
        }

        // POST api/splits
        [HttpPost]
        public SplitDto Create([FromBody]SplitCreationDto splitCreationDto)
        {
            var splitDto = new SplitDto { Name = splitCreationDto.Name, Created = splitCreationDto.Created };
            splitDto.Id = GetNextSplitId();

            _splits.Add(splitDto);

            return splitDto;
        }
        private int GetNextSplitId()
        {
            return _splits.Max(s => s.Id) + 1;
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
