using AizenBankV1.BusinessLogic.DBModel.Seed;
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

        public ActionResult Tables()
        {
            List<UserData> allUsers = GetAllUsers();

            // Pass the list of users to the view
            return View(allUsers);
        }

        List<UserData> GetAllUsers()
        {
            // Perform a database query to retrieve all users
            // This might vary depending on your database technology (e.g., Entity Framework, ADO.NET)
            // Here's a pseudo example assuming Entity Framework
            using (var dbContext = new UserContext())
            {
                return dbContext.Users.Select(u => new UserData
                {
                    Name = u.UserName,
                    Email = u.Email
                }).ToList();
            }
        }
    }
}