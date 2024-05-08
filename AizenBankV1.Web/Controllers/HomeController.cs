using AizenBankV1.BusinessLogic;
using AizenBankV1.BusinessLogic.DBModel.Seed;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Entities.Card;
using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Web.AdminAttributes;
using AizenBankV1.Web.Extensions;
using AizenBankV1.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AizenBankV1.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISession _session;
        public HomeController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }

        private readonly UserContext _userContext;
        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("LogIn", "Register");
            }
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData userData = new UserData
            {
                Name = user.Username,
                Email = user.Email
            };

            return View(userData);
        }


        public ActionResult Profile()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData userData = new UserData
            {
                Name = user.Username,
                Email = user.Email
            };

            return View(userData);
        }

        public ActionResult ActivityLog()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData userData = new UserData
            {
                Name = user.Username,
                Email = user.Email
            };

            return View(userData);
        }

        public ActionResult Settings()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData userData = new UserData
            {
                Name = user.Username,
                Email = user.Email
            };

            return View(userData);
        }

        [HttpGet]
        public ActionResult CardsAccounts()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);
            return View(cards);
        }

        public ActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCard(CardMinimal cardInfo)
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);
            if(cards.Count >= 4)
            {
                return RedirectToAction("CardsAccount", "Home");
            }

            _session.CreateCard(cardInfo, currentUser);
            return RedirectToAction("CardsAccounts", "Home");
        }

        public ActionResult Deposit()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);

            var depositDataModel = new DepositDataModel
            {
                UserCards = cards, // Initialize UserCards property with actual user cards
                Money = 0 // Initial value for Money, adjust as needed
            };

            return View(depositDataModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(DepositDataModel data)
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            data.UserCards = _session.GetCards(currentUser);
            var selectedCard = data.UserCards.FirstOrDefault(c => c.Name == data.SelectedCardName);

            if (selectedCard != null)
            {
                var depositData = new DepositData
                {
                    CardName = selectedCard.Name,
                    Money = data.Money
                };
                _session.Deposit(depositData);
                return RedirectToAction("DepositSuccess", "Home");
            }
            else
            {
                ModelState.AddModelError("SelectedCardName", "Selected card not found.");
            }

            return View(data);
        }

        public ActionResult DepositSuccess()
        {
            return View();
        }

    }
}