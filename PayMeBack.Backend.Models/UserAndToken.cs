using System;

namespace PayMeBack.Backend.Models
{
    public class UserAndToken
    {
        public AppUser User { get; set; }

        public string Token { get; set; }
    }
}
