using AizenBankV1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.User
{
    public class URegisterData
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime LastLogin { get; set; }

        public string LasIp { get; set; }

        public URole Level { get; set; }
    }
}
