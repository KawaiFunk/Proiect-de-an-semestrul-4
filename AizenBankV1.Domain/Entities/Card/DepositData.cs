using AizenBankV1.Domain.Entities.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.Models
{
    public class DepositData
    {
        public double Money { get; set; }
        public string CardName { get; set; }
    }
}