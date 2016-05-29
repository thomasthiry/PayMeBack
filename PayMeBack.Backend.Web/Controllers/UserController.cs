using AutoMapper;
using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Web.Models;
using System;

namespace PayMeBack.Backend.Web.Controllers
{
    public class UserController : Controller
    {
        private IMapper _mapper;
        private IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        public UserAndTokenDto Login([FromBody]LoginRequestDto loginRequest)
        {
            return _mapper.Map<UserAndTokenDto>(_userService.Login(loginRequest.Email, loginRequest.Password));
        }

        [HttpPost]
        public UserDto UserCreate([FromBody]UserCreationRequestDto userCreationRequest)
        {
            return _mapper.Map<UserDto>(_userService.Create(userCreationRequest.Email, userCreationRequest.Name, userCreationRequest.Password));
        }
    }
}
