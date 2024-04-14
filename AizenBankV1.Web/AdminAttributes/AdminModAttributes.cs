using AizenBankV1.BusinessLogic;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Enums;
using AizenBankV1.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AizenBankV1.Web.AdminAttributes
{
    public class AdminModAttributes : ActionFilterAttribute
    {
        private readonly ISession _sessionBussinesLogic;

        public AdminModAttributes()
        {
            var bussinesLogic = new BussinessLogic();
            _sessionBussinesLogic = bussinesLogic.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _sessionBussinesLogic.GetUserByCookie(apiCookie.Value);
                if (profile == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Register", action = "LogIn" }));
                }

                if(profile.Level != URole.admin)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Home", action = "Index" }));
                }

                if (profile.Level == URole.admin)
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Register", action = "LogIn" }));
            }
        }
    }
}