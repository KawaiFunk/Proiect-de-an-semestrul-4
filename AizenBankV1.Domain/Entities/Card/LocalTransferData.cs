using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.Card
{
    public class LocalTransferData
    {
        public double Money { get; set; }
        public string SourceCardName { get; set; }
        public string DestinationCardName { get; set; }
    }
}
