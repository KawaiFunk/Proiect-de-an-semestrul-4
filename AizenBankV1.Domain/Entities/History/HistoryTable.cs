using AizenBankV1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.History
{
    public class HistoryTable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public HistoryType Type { get; set; }
    }
}
