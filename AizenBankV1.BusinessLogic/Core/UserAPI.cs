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
using AizenBankV1.Domain.Entities.History;

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
                using (var history = new HistoryContext())
                {
                    var historityInfo = new HistoryTable
                    {
                        Source = "External card",
                        Destination = card.Name,
                        Amount = card.MoneyAmount,
                        DateTime = DateTime.Now,
                        Type = HistoryType.Deposit
                    };
                    history.Histories.Add(historityInfo);
                    history.SaveChanges();
                }
                    db.SaveChanges();
            }
        }

        public void RWithdraw(DepositData data)
        {
            using (var db = new CardsContexts())
            {
                var card = db.UsersCards.FirstOrDefault(u => u.Name == data.CardName);
                card.MoneyAmount -= data.Money;
                using (var history = new HistoryContext())
                {
                    var historityInfo = new HistoryTable
                    {
                        Source = card.Name,
                        Destination = "External card",
                        Amount = data.Money,
                        DateTime = DateTime.Now,
                        Type = HistoryType.Withdraw
                    };
                    history.Histories.Add(historityInfo);
                    history.SaveChanges();
                }
                db.SaveChanges();
            }
        }

        public void RLocalTranfer(LocalTransferData data)
        {
            using (var db = new CardsContexts())
            {
                var sourceCard = db.UsersCards.FirstOrDefault(u => u.Name == data.SourceCardName);
                var destinationCard = db.UsersCards.FirstOrDefault(u => u.Name == data.DestinationCardName);
                sourceCard.MoneyAmount -= data.Money;
                destinationCard.MoneyAmount += data.Money;
                using (var history = new HistoryContext())
                {
                    var historityInfo = new HistoryTable
                    {
                        Source = sourceCard.Name,
                        Destination = destinationCard.Name,
                        Amount = data.Money,
                        DateTime = DateTime.Now,
                        Type = HistoryType.LocalTransfer
                    };
                    history.Histories.Add(historityInfo);
                    history.SaveChanges();
                }
                db.SaveChanges();
            }
        }

        public void RTransfer(TransferData data)
        {
            using (var users = new UserContext())
            {
                using (var db = new CardsContexts())
                {
                    var sourceCard = db.UsersCards.FirstOrDefault(u => u.Name == data.SourceCard);
                    var sourceUser = data.SourceEmail;
                    var destUser = users.Users.FirstOrDefault(u => u.Email == data.DestinationCard);
                    var destUserMinimal = new UserMinimal
                    {
                        Email = destUser.Email,
                        Id = destUser.Id,
                        LasIp = destUser.LasIp,
                        LastLogin = destUser.LastLogin,
                        Level = destUser.Level,
                        Password = destUser.Password,
                        Username = destUser.UserName
                    };
                    List<CardMinimal> destCards = RGetCards(destUserMinimal);
                    if (destCards == null || destCards.Count == 0)
                    {
                        throw new InvalidOperationException("Destination user does not have any open cards.");
                    }
                    var destCard = destCards[0];
                    var destCardDB = db.UsersCards.FirstOrDefault(u => u.Name.Equals(destCard.Name));
                    destCardDB.MoneyAmount += data.Money;
                    sourceCard.MoneyAmount -= data.Money;
                    
                    using (var history = new HistoryContext())
                    {
                        var historityInfo = new HistoryTable
                        {
                            Source = sourceCard.Name,
                            Destination = data.DestinationCard,
                            Amount = data.Money,
                            DateTime = DateTime.Now,
                            Type = HistoryType.Transfer
                        };
                        history.Histories.Add(historityInfo);

                        var historyDestinationInfo = new HistoryTable
                        {
                            Source = sourceUser,
                            Destination = destCard.Name,
                            Amount = data.Money,
                            DateTime = DateTime.Now,
                            Type = HistoryType.Transfer
                        };
                        history.Histories.Add(historyDestinationInfo);

                        history.SaveChanges();
                    }
                    db.SaveChanges();
                } 
            }  
        }

        public List<HistoryTable> RGetHistory(UserMinimal user)
        {
            using (var cardsDB = new CardsContexts())
            {
                using (var db = new HistoryContext())
                {
                    List<HistoryTable> histories = new List<HistoryTable>();

                    List<CardMinimal> userCards = RGetCards(user);

                    foreach (var card in userCards)
                    {
                        var matchingHistories = db.Histories
                            .Where(history => history.Source == card.Name || history.Destination == card.Name)
                            .ToList();

                        foreach (var history in matchingHistories)
                        {
                            if (!histories.Any(h => h.Id == history.Id))
                            {
                                histories.Add(history);
                            }
                        }
                    }

                    histories = histories.OrderBy(history => history.DateTime).ToList();

                    return histories;
                }
            }
        }
    }
}
