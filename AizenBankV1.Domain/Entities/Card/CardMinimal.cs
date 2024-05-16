using AizenBankV1.Domain.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.Card
{
    public class CardMinimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double MoneyAmount { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public CardTypes CardType { get; set; }

        public string SelectedCardName { get; set; }
    }
}
