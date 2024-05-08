using AizenBankV1.Domain.Entities.Card;
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
    public class CardsContexts : DbContext
    {
        public CardsContexts() : base("name=AizenBankV1")
        {
        }

        public DbSet<CardsDBTable> UsersCards { get; set; }
    }
}
