using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using System.Web;
using AizenBankV1.Domain.Entities.Card;
using AizenBankV1.Web.Models;

namespace AizenBankV1.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULogInResponce UserLoginAction(ULoginData data);
        URegisterResponce UserRegisterAction(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string value);
        string SendCode(string password);
        void ChangePassword(string pass, string email);
        bool UserExists(string email);
        List<CardMinimal> GetCards(UserMinimal user);
        void CreateCard(CardMinimal cardInfo, UserMinimal user);
        void Deposit(DepositData data);
    }
}
