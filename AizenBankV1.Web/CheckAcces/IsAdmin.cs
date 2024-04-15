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
            // Check if the user is authenticated
                // Assuming you have a User model with a Role property
                var currentUser = HttpContext.Current.GetMySessionObject(); // Assuming this gives the username

                if(currentUser.Level == Domain.Enums.URole.admin)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            return false; // Default to false if not authenticated or not an admin
        }
    }
}