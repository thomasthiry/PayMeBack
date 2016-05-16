using System;

namespace PayMeBack.Backend.Web.Models
{
    public class SplitContactUpdateDto
    {
        public decimal Owes { get; set; }

        public decimal Paid { get; set; }

        public string Iban { get; set; }

        public string Address { get; set; }

        public string Comments { get; set; }
    }
}
