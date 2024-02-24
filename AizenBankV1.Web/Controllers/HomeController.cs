using AizenBankV1.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AizenBankV1.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            CardInfo card1 = new CardInfo();
            card1.CardNumber = "4669 2312 5342 2313";
            card1.ExpirationDate = "23/29";
            card1.CurrencyAmount = 23523.32;
            card1.Transfers = new List<double> { -2341, 5123, -32.43, 12345.32 };

            UserData user = new UserData();
            user.Name = "Damian Dan";
            user.Email = "TestEmail";

            return View(user);
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult ActivityLog()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult CardsAccounts()
        {
            return View();
        }
    }
}