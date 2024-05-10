using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.Models
{
    public class ProfileData
    {
        public double Money { get; set; }
        public int OpenCards { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}