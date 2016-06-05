using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Web.Models;
using PayMeBack.Backend.Contracts.Services;
using AutoMapper;
using System.Security.Claims;
using System;

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
            var userId = Convert.ToInt32(HttpContext.User.GetUserId());
            return _mapper.Map<IEnumerable<SplitDto>>(_splitService.List(userId));
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

        // GET splits/5/settle
        [HttpGet]
        public SettlementDto Settle([FromRoute]int id)
        {
            return _mapper.Map<SettlementDto>(_splitService.Settle(id));
        }
    }
}
