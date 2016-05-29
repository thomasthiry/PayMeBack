using System;

namespace PayMeBack.Backend.Web.Models
{
    public class UserAndTokenDto
    {
        public UserDto User { get; set; }

        public string Token { get; set; }
    }
}
