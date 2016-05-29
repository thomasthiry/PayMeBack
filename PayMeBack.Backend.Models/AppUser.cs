using System;

namespace PayMeBack.Backend.Models
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime Creation { get; set; }
    }
}
