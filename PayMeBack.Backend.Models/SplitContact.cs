using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayMeBack.Backend.Models
{
    public class SplitContact : IEntity
    {
        public int Id { get; set; }

        public int SplitId { get; set; }
        public Split Split { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public decimal Owes { get; set; }

        public decimal Paid { get; set; }

        [NotMapped]
        public decimal PaidBalance => Paid - Owes;

        public string Comments { get; set; }
    }
}
