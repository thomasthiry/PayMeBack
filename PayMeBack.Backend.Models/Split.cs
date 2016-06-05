using System;

namespace PayMeBack.Backend.Models
{
    public class Split : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
