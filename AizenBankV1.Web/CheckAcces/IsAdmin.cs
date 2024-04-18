using AizenBankV1.BusinessLogic.DBModel.Seed;
using AizenBankV1.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AizenBankV1.Web.CheckAcces
{
    public class IsAdmin
    {
        public static bool IsUserAdmin()
        {
                var currentUser = HttpContext.Current.GetMySessionObject();

                if(currentUser.Level == Domain.Enums.URole.admin)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            return false;
        }
    }
}