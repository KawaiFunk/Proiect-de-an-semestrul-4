using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.Models
{
    public class UserLogin
    {
        public string Credentials { get; set; }
        public string Password { get; set; }
        public string LogInIP { get; set; }
        public DateTime LogInFateTime { get; set; }
    }
}