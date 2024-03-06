using AizenBankV1.BusinessLogic.Core;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.BusinessLogic
{
    public class SessionBL : UserAPI, ISession
    {
        public ULogInResponce UserLoginAction(ULoginData data)
        {
            return RLogInUpService(data);
        }
    }
}
