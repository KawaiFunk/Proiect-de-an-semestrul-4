﻿using AizenBankV1.Web.AdminAttributes;
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

        [AdminModAttributes]
        public ActionResult CardsAccounts()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData userData = new UserData
            {
                Name = user.Username,
                Email = user.Email
            };

            return View(userData);
        }
    }
}