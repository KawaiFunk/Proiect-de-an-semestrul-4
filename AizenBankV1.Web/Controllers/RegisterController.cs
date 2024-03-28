using AizenBankV1.BusinessLogic;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using AizenBankV1.Domain.Entities.Responces;

namespace AizenBankV1.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ISession _session;
        public RegisterController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult LogIn(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credentials = login.Credentials,
                    Password = login.Password
                };

                ULogInResponce resp = _session.UserLoginAction(data);
                if (resp.Status)
                {
                    //Add Cookie

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", resp.ActionStatusMsg);
                    return View();
                }
            }

            return View();
        }
            
        


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}