using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AizenBankV1.Domain.Session;

namespace AizenBankV1.BusinessLogic.DBModel.Seed
{
    internal class SessionContext : DbContext
    {
        public SessionContext() :
             base("name=AizenBankV1")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
