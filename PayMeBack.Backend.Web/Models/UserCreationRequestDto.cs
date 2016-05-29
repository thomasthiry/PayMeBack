using System;

namespace PayMeBack.Backend.Web.Models
{
    public class UserCreationRequestDto
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
