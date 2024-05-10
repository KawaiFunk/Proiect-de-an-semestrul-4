using AizenBankV1.Domain.Entities.Card;
using AizenBankV1.Domain.Entities.History;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.BusinessLogic.DBModel.Seed
{
    public class HistoryContext : DbContext
    {
        public HistoryContext() : base("name=AizenBankV1")
        {
        }

        public DbSet<HistoryTable> Histories { get; set; }
    }
}
