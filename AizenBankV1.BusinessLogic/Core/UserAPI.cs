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
using proiect.Helpers;
using System.Web.Configuration;
using AizenBankV1.Domain.Entities.Card;
using System.Web;
using AizenBankV1.Web.Models;

namespace AizenBankV1.BusinessLogic.Core
{
    public class UserAPI
    {
        public ULogInResponce RLogInUpService(ULoginData data)
        {

            UDbTable user;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credentials))
            {
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(us => us.Email == data.Credentials);
                }

                if (user != null)
                {
                    var hashedPassword = LoginHelper.HashGen(data.Password);
                    if (user != null && user.Password == hashedPassword)
                    {
                        using (var db = new UserContext())
                        {
                            user.LasIp = data.LogInIP;
                            user.LastLogin = data.LogInDateTime;
                            db.Entry(user).State = EntityState.Modified;
                        }


                        if (user.Level == URole.user)
                            return new ULogInResponce { Status = true, Message = "user" };
                        else
                             if (user.Level == URole.admin)
                            return new ULogInResponce { Status = true, Message = "admin" };
                    }
                }
            }
            else
            {
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(us => us.UserName == data.Credentials);
                }

                if (user != null)
                {
                    var hashedPassword = LoginHelper.HashGen(data.Password);
                    if (user != null && user.Password == hashedPassword)
                    {
                        using (var db = new UserContext())
                        {
                            user.LasIp = data.LogInIP;
                            user.LastLogin = data.LogInDateTime;
                            db.Entry(user).State = EntityState.Modified;
                            //db.SaveChanges();
                        }


                        if (user.Level == URole.user)
                            return new ULogInResponce { Status = true, Message = "user" };
                        else
                             if (user.Level == URole.admin)
                            return new ULogInResponce { Status = true, Message = "admin" };
                    }
                }
            }

            

            // Authentication failed
            return new ULogInResponce { Status = false , Message = "none"};
        }

        public URegisterResponce RRegisterUpService(URegisterData data)
        {
            using (var db = new UserContext())
            {
                bool existingUsername = db.Users.Any(u => u.UserName == data.UserName);
                bool existingEmail = db.Users.Any(u => u.Email == data.Email);

                if (existingUsername || existingEmail)
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

        public string RSendCode(string input)
        {
            string code = GeneratePasscode.Generate(6);
            SendEmail.SendEmailCode(input, code);
            return code;
        }

        public void RChangePassword(string pass, string email)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                string encryptedPassword = LoginHelper.HashGen(pass);
                user.Password = encryptedPassword;
                db.SaveChanges();
            }
        }

        public bool RUserExists(string email)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                if (user == null) return false;
            }
            return true;
        }

        public List<CardMinimal> RGetCards(UserMinimal user)
        {
            using (var db = new CardsContexts())
            {
                List<CardMinimal> cards = new List<CardMinimal>();

                var matchingCards = db.UsersCards.Where(card => card.userID == user.Id).ToList();

                foreach (var cdbCard in matchingCards)
                {
                    CardMinimal cardMinimal = ConvertToCardMinimal(cdbCard);
                    cards.Add(cardMinimal);
                }

                return cards;
            }
        }

        private CardMinimal ConvertToCardMinimal(CardsDBTable cdbCard)
        {
            CardMinimal cardMinimal = new CardMinimal();
            cardMinimal.Name = cdbCard.Name;
            cardMinimal.UserID = cdbCard.userID;
            cardMinimal.Description = cdbCard.Description;
            cardMinimal.ExpirationDate = cdbCard.ExpireDate;
            cardMinimal.MoneyAmount = cdbCard.MoneyAmount;
            cardMinimal.CardType = cdbCard.CardType;
            return cardMinimal;
        }

        public void RCreateCard(CardMinimal cardInfo, UserMinimal user)
        {
            var card = new CardsDBTable
            {
                userID = user.Id,
                Name = cardInfo.Name,
                MoneyAmount = 0,
                CardType = CardTypes.Credit,
                ExpireDate = DateTime.Now.AddYears(4),
                Description = cardInfo.Description 
            };
            using (var db = new CardsContexts()) 
            {
                db.UsersCards.Add(card);
                db.SaveChanges();
            }
        }
        
        public void RDeposit(DepositData data)
        {
            using (var db = new CardsContexts())
            {
                var card = db.UsersCards.FirstOrDefault(u => u.Name == data.CardName);
                card.MoneyAmount += data.Money;
                db.SaveChanges();
            }
        }
    }
}
