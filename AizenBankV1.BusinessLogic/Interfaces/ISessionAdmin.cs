using AizenBankV1.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.BusinessLogic.Interfaces
{
    public interface ISessionAdmin
    {
        List<UserMinimal> RGetAllUsers();
        UserMinimal RGetUserById(int id);
        void EditUser(int id, UserMinimal user);
    }
}
