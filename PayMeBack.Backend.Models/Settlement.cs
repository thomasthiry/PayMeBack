using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Models
{
    public class Settlement
    {
        public Split Split { get; set; }
        public List<SettlementTransfer> Transfers { get; set; }

        public Settlement()
        {
            Transfers = new List<SettlementTransfer>();
        }
    }
}
