using System;

namespace PayMeBack.Backend.Models
{
    public class SettlementTransfer
    {
        public Contact FromContact { get; set; }

        public Contact ToContact { get; set; }

        public decimal Amount { get; set; }
    }
}
