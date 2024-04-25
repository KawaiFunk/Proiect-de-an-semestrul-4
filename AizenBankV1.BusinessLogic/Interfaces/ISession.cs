using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using System.Web;

namespace AizenBankV1.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULogInResponce UserLoginAction(ULoginData data);
        URegisterResponce UserRegisterAction(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string value);
        string SendCode(string password);
    }
}
