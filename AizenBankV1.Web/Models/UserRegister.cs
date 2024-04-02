using AizenBankV1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.Models
{
    public class UserRegister
    {

        public string Credentials { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}