using AizenBankV1.BusinessLogic.Core;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AizenBankV1.BusinessLogic
{
    public class SessionBL : UserAPI, ISession
    {
        public ULogInResponce UserLoginAction(ULoginData data)
        {
            return RLogInUpService(data);
        }

        public URegisterResponce UserRegisterAction(URegisterData data)
        {
            return RRegisterUpService(data);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public string SendCode(string input)
        {
            return RSendCode(input);
        }
        
    }
}
