﻿using AizenBankV1.Domain.Entities.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.Models
{
    public class TransferModel
    {
        public double Money { get; set; }
        public string SourceCard { get; set; }
        public string DestinatorEmail { get; set; }
        public List<CardMinimal> Cards { get; set; }
    }
}