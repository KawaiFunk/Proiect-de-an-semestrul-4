using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.BusinessLogic.Core
{
    public class UserAPI
    {
        public ULogInResponce RLogInUpService(ULoginData data)
        {
            //SQL DATA
            return new ULogInResponce { Status = false };
        }
    }
}
