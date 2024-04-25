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
using AutoMapper;
using Microsoft.Win32;
using AizenBankV1.BusinessLogic.DBModel.Seed;
using AizenBankV1.Helpers;

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

            HttpContext.Session["UserProfile"] = login;
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

                var data = Mapper.Map<ULoginData>(login);
                data.LogInIP = Request.UserHostAddress;
                data.LogInDateTime = DateTime.Now;


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
                    ViewBag.ErrorMessage = "Username or password is incorrect.";
                    return View(login);
                }
            }

            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword([Bind(Include = "Credentials")] UserLogin input)
        {
            if (input.Credentials != null)
            {
                string code = _session.SendCode(input.Credentials);
                TempData["Email"] = input.Credentials;
                TempData["code"] = code;
            }
            return RedirectToAction("ConfirmCode", "Register");
        }

        public ActionResult ConfirmCode()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmCode([Bind(Include = "Code")] VerificationCode input)
        {
            string email = TempData["Email"] as string;
            string verificationCode = TempData["code"] as string;
            string code = input.Code;
            UDbTable user;

            // Check if the verification code matches the one sent
            if (code != null && code.Equals(verificationCode))
            {
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == email);
                }
                if (user != null)
                {
                    // If the user is found, proceed to the ResetPassword action
                    return RedirectToAction("ResetPassword", new { email = email });
                }
                else
                {
                    ViewBag.ErrorMessage = "User not found.";
                    return View("ForgotPassword");
                }
            }
            else
            {
                TempData.Remove("Email");
                TempData.Remove("code");

                TempData["ErrorMessage"] = "Incorrect verification code.";
                return RedirectToAction("ForgotPassword", "Register");
            }
        }


        public ActionResult ResetPassword()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ForgotPassword model)
        {
                var _dbContext = new UserContext();
                string email = TempData["Email"] as string;

                if (model.NewPassword == model.ConfirmPassword)
                {
                    // Access email from the model
                    var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

                    if (user != null)
                    {
                        string encryptedPassword = LoginHelper.HashGen(model.NewPassword); // Use the new password from the model
                        user.Password = encryptedPassword;
                        _dbContext.SaveChanges();

                        return RedirectToAction("LogIn", "Register");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User not found.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Passwords do not match.";
                }

            // Return the view with the model to display validation errors
            return View(model);
        }


    }
}