using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Models
{
    public class Contact : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Iban { get; set; }

        public string Address { get; set; }
    }
}
