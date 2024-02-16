using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AizenBankV1.Web.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult LogIn()
        {
            return LogIn();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}