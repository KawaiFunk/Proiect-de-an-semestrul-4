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
using System.Data;
using System.Web.Security;

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

        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            if (Response.Cookies["X-KEY"] != null)
            {
                var cookie = new HttpCookie("X-KEY")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = true
                };
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("LogIn", "Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult LogIn(UserLogin login)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            if (Response.Cookies["X-KEY"] != null)
            {
                var cookie = new HttpCookie("X-KEY")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = true
                };
                Response.Cookies.Add(cookie);
            }

            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credentials = login.Credentials,
                    Password = login.Password,
                    LogInDateTime = DateTime.Now,
                    LogInIP = Request.UserHostAddress
                };

                ULogInResponce resp = _session.UserLoginAction(data);
                if (resp.Status)
                {
                    HttpCookie cookie = _session.GenCookie(data.Credentials);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["UserName"] = data.Credentials;

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