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