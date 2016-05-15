using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Web.Models
{
    public class SettlementTransferDto
    {
        public ContactDto FromContact { get; set; }

        public ContactDto ToContact { get; set; }

        public decimal Amount { get; set; }
    }
}
