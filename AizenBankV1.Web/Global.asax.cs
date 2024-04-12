using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace AizenBankV1.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           RouteConfig.RegisterRoutes(RouteTable.Routes);

           BundleConfig.RegisterBundle(BundleTable.Bundles);
            Mapper.Initialize(cfg => {
                cfg.CreateMap<UDbTable, UserMinimal>();
                cfg.CreateMap<UserLogin, ULoginData>();
            });
        }
    }
}