using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Web.Models
{
    public class SettlementDto
    {
        public SplitDto Split { get; set; }

        public List<SettlementTransferDto> Transfers { get; set; }
    }
}
