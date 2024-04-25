using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.Card
{
    public class CDbTable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MoneyAmount { get; set; }
    }
}
