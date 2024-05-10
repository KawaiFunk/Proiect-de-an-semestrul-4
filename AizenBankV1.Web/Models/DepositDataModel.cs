using AizenBankV1.Domain.Entities.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.Models
{
    public class DepositDataModel
    {
        public double Money { get; set; }
        public string SelectedCardName { get; set; }
        public List<CardMinimal> UserCards { get; set; }
    }
}