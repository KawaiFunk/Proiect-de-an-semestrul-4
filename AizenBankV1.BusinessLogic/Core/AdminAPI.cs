using AizenBankV1.BusinessLogic.DBModel.Seed;
using AizenBankV1.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.BusinessLogic.Core
{
    internal class AdminAPI
    {
        public List<UserMinimal> RGetAllUsers()
        {
            using (var dbContext = new UserContext())
            {
                return dbContext.Users.Select(u => new UserMinimal
                {
                    Username = u.UserName,
                    Email = u.Email,
                    Password = u.Password,
                    Id = u.Id,
                    LasIp = u.LasIp,
                    LastLogin = u.LastLogin,
                    Level = u.Level,
                }).ToList();
            }
        }

        public UserMinimal RGetUserById(int Id)
        {
            using (var dbContext = new UserContext())
            {
                var user = dbContext.Users.FirstOrDefault(us => us.Id == Id);
                if (user == null) return null;
                return new UserMinimal
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Level = user.Level,
                    Id = user.Id,
                };
            }
        }

        public void REditUser(int Id, UserMinimal userModel)
        {
            using (var dbContext = new UserContext())
            {
                var user = dbContext.Users.FirstOrDefault(us => us.Id == Id);
                if (user == null) return;

                user.UserName = userModel.Username;
                user.Email = userModel.Email;
                user.Level = userModel.Level;

                dbContext.SaveChanges();
            }
        }

        public void RDeleteUser(int id)
        {
            using (var dbContext = new UserContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
                if (user == null) return;

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }
    }
}
