using System;

namespace PayMeBack.Backend.Web.Models
{
    public class SplitContactDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public decimal Owes { get; set; }

        public decimal Paid { get; set; }

        public string Comments { get; set; }
    }
}
