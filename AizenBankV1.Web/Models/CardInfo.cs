using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.Models
{
    public class CardInfo
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public double CurrencyAmount { get; set; }
        public List<double> Transfers { get; set; }
    }
}