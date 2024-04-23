using AizenBankV1.BusinessLogic;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Web.AdminAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AizenBankV1.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        private readonly ISessionAdmin _session;

        public AdminController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetAdminSessionBL();
        }

        [AdminModAttributes]
        public ActionResult Tables()
        {
            List<UserMinimal> allUsers = _session.RGetAllUsers();
            return View(allUsers);
        }

        [AdminModAttributes]
        [HttpGet]
        [Route("Admin/EditUserInfo/{id}")]
        public ActionResult EditUserInfo(int id)
        {
            var userFromDB = _session.RGetUserById(id);
            if (userFromDB == null)
            {
                return View();
            }
            else
            {
                return View("EditUserInfo", userFromDB);
            }
        }

        [AdminModAttributes]
        [HttpPost]
        [Route("Admin/EditUserInfo/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfoConfirm(int id, UserMinimal userModel)
        {
            if (ModelState.IsValid)
            {
                _session.EditUser(id, userModel);
                return RedirectToAction("Tables");
            }
            return View("EditUserInfo", userModel); 
        }

        [AdminModAttributes]
        [HttpPost]
        [Route("Admin/DeleteUser/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            if (ModelState.IsValid)
            {
                _session.DeleteUser(id);
                return RedirectToAction("Tables");
            }
            return RedirectToAction("Tables");
        }
    }
}