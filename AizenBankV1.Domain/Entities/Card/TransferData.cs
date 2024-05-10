using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.Card
{
    public class TransferData
    {
        public double Money { get; set; }
        public string SourceCard { get; set; }
        public string SourceEmail { get; set; }
        public string DestinationCard { get; set; }
    }
}
