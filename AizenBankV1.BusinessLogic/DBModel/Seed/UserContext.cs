using AizenBankV1.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.BusinessLogic.DBModel.Seed
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=AizenBankV1")
        {
        }

        public DbSet<UDbTable> Users { get; set; }
    }
}
