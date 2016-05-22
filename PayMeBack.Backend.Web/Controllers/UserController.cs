using Microsoft.AspNet.Mvc;
using PayMeBack.Backend.Web.Models;

namespace PayMeBack.Backend.Web.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public TokenDto Login([FromBody]LoginRequestDto loginRequest)
        {
            return new TokenDto { Token = "IJ9RJZR908JIZ" };
        }
    }
}
