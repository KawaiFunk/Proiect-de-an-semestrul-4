using AizenBankV1.BusinessLogic.DBModel.Seed;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AizenBankV1.Helpers;
using AizenBankV1.Domain.Session;
using AutoMapper;

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
            }

            if (user != null)
            {
                var hashedPassword = LoginHelper.HashGen(data.Password);
                if (user!= null && user.Password == hashedPassword)
                {
                    using (var db = new UserContext())
                    {
                        user.LasIp = data.LogInIP;
                        user.LastLogin = data.LogInDateTime;
                        db.Entry(user).State = EntityState.Modified;
                        //db.SaveChanges();
                    }


                    if (user.Level == URole.user)
                        return new ULogInResponce { Status = true , Message = "user"};
                    else
                         if (user.Level == URole.admin)
                        return new ULogInResponce { Status = true , Message = "admin" };
                }
            }

            // Authentication failed
            return new ULogInResponce { Status = false , Message = "none"};
        }

        public URegisterResponce RRegisterUpService(URegisterData data)
        {
            using (var db = new UserContext())
            {
                bool existingUser = db.Users.Any(u => u.UserName == data.UserName);

                if (existingUser)
                {
                    return new URegisterResponce { Status = false };
                }

                var newUser = new UDbTable();
                newUser.UserName = data.UserName;
                newUser.Password = LoginHelper.HashGen(data.Password);
                newUser.Email = data.Email;
                newUser.LastLogin = DateTime.Now;
                newUser.Level = Domain.Enums.URole.user;
                newUser.LasIp = "0.0.0.0";

                db.Users.Add(newUser);
                db.SaveChanges();
                return new URegisterResponce { Status = true };
            }
        }

        public System.Web.HttpCookie Cookie(string credential)
        {
            var apiCookie = new System.Web.HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(credential)
            };
            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(credential))
                {
                    curent = (from e in db.Sessions where e.UserName == credential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.UserName == credential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        UserName = credential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }
        public UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.UserName))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.UserName);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.UserName == session.UserName);
                }
            }

            if (curentUser == null) return null;
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}
