using AizenBankV1.BusinessLogic.DBModel.Seed;
using AizenBankV1.Domain.Entities.Card;
using AizenBankV1.Domain.Entities.History;
using AizenBankV1.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.BusinessLogic.Core
{
    internal class AdminAPI : UserAPI
    {
        public List<UserMinimal> RGetAllUsers()
        {
            using (var dbContext = new UserContext())
            {
                return dbContext.Users.Select(u => new UserMinimal
                {
                    Username = u.UserName,
                    Email = u.Email,
                    Password = u.Password,
                    Id = u.Id,
                    LasIp = u.LasIp,
                    LastLogin = u.LastLogin,
                    Level = u.Level,
                }).ToList();
            }
        }

        public UserMinimal RGetUserById(int Id)
        {
            using (var dbContext = new UserContext())
            {
                var user = dbContext.Users.FirstOrDefault(us => us.Id == Id);
                if (user == null) return null;
                return new UserMinimal
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Level = user.Level,
                    Id = user.Id,
                };
            }
        }

        public void REditUser(int Id, UserMinimal userModel)
        {
            using (var dbContext = new UserContext())
            {
                var user = dbContext.Users.FirstOrDefault(us => us.Id == Id);
                if (user == null) return;

                user.UserName = userModel.Username;
                user.Email = userModel.Email;
                user.Level = userModel.Level;

                dbContext.SaveChanges();
            }
        }

        public void RDeleteUser(int id)
        {
            using (var dbContext = new UserContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
                if (user == null) return;

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
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
    }
}
