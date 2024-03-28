using AizenBankV1.BusinessLogic.DBModel.Seed;
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
            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(us => us.UserName == data.Credentials);
                if (user == null) return new ULogInResponce { Status = false };
                else
                {
                    if (user.Password == data.Password)
                        return new ULogInResponce { Status = true };
                }
            }
            return new ULogInResponce { Status = false };
        }
    }
}
