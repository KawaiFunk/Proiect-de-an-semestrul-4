using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;

namespace AizenBankV1.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULogInResponce UserLoginAction(ULoginData data);
    }
}
