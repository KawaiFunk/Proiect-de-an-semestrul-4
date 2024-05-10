using AizenBankV1.BusinessLogic.Core;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Entities.Card;
using AizenBankV1.Domain.Entities.History;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Web.Models;
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

        public void ChangePassword(string pass, string email)
        {
            RChangePassword(pass, email);
        }

        public bool UserExists(string email)
        {
            return RUserExists(email);
        }

        public List<CardMinimal> GetCards(UserMinimal user)
        {
            return RGetCards(user);
        }

        public void CreateCard(CardMinimal cardInfo, UserMinimal user)
        {
            RCreateCard(cardInfo, user);
        }

        public void Deposit(DepositData data)
        {
            RDeposit(data);
        }
        
        public void Withdraw(DepositData data)
        {
            RWithdraw(data);
        }

        public void LocalTransfer(LocalTransferData data)
        {
            RLocalTranfer(data);
        }

        public void Transfer(TransferData data)
        {
            RTransfer(data);
        }

        public List<HistoryTable> GetHistory(UserMinimal user)
        {
            return RGetHistory(user);
        }
    }
}
