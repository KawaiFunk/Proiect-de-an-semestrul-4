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

        public URegisterResponce RRegisterUpService(URegisterData data)
        {
            var newUser = new UDbTable();
            newUser.Id = 1232;
            newUser.UserName = data.UserName;
            newUser.Password = data.Password;
            newUser.Email = data.Email;
            newUser.LastLogin = DateTime.Now;
            newUser.Level = Domain.Enums.URole.user;
            newUser.LasIp = "data.LasIp";

            using (var db = new UserContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            return new URegisterResponce { Status = true };
        }
    }
}
