using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.User
{
    public class ULoginData
    {
        public string Credentials { get; set; }
        public string Password { get; set; }
        public string LogInIP { get; set; }
        public DateTime LogInDateTime {  get; set; }
    }
}
