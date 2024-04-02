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
            if (ModelState.IsValid)
            {
                URegisterData data = new URegisterData
                {
                    UserName = registers.Credentials,
                    Password = registers.Password,
                    Email = registers.Email
                };

                URegisterResponce resp = _session.UserRegisterAction(data);
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
    }
}