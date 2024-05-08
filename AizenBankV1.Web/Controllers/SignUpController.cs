using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AizenBankV1.Domain.Entities.Responces;
using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Web.Models;

namespace AizenBankV1.Web.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ISession _session;
        public SignUpController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SignUp(UserRegister registers)
        {
            ViewBag.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                URegisterData data = new URegisterData
                {
                    UserName = registers.Credentials,
                    Password = registers.Password,
                    Email = registers.Email,
                    LasIp = Request.UserHostAddress,
                    LastLogin = DateTime.Now,
                    Level = Domain.Enums.URole.user
                };

                URegisterResponce resp = _session.UserRegisterAction(data);
                if (resp.Status)
                {
                    ULoginData user = new ULoginData
                    {
                        Credentials = data.UserName,
                        Password = data.Password
                    };

                    _session.UserLoginAction(user);
                    HttpCookie cookie = _session.GenCookie(user.Credentials);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Username or email is already in use.";
                    return View(registers);
                }
            }



            return View();
        }
    }
}