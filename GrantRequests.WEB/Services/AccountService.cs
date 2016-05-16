using GrantRequests.DAL.Entities;
using GrantRequests.DAL.Repositories;
using GrantRequests.Common;
using GrantRequests.WEB.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GrantRequests.WEB.Services
{
    public class AccountService:ServiceBase
    {
        public AccountService(UnitOfWork unitOfWork):base(unitOfWork) { }

        public bool IsUserExists(string userName)
        {
            var user = unitOfWork.Users.Find(u => u.Name == userName).SingleOrDefault();
            return user != null;
        }
        public bool IsUserExists(string userName, string password)
        {
            var user = unitOfWork.Users.Find(u => u.Name == userName && u.Password == password).SingleOrDefault();
            return user != null; 
        }

        public bool LogInUser(string userName, string password)
        {
            var currentUser = unitOfWork.Users.Find(u => u.Name == userName && u.Password == password)
                .SingleOrDefault();
            if (currentUser != null)
            {
                FormsAuthentication.SetAuthCookie(
                    string.Format(
                      "{0}" 
                    + IdentityExtensions.identitySeparator 
                    + "{1}" 
                    + IdentityExtensions.identitySeparator
                    + "{2}",
                    currentUser.Name, currentUser.Role, currentUser.Id), true);
                return true;
            }
            return false;
        }

        public void LogOffUser()
        {
            FormsAuthentication.SignOut();
        }

        public void RegisterUser(RegisterViewModel model)
        {
            var user = AutoMapper.Mapper.Map<User>(model);
            unitOfWork.Users.Create(user);
            unitOfWork.Save();
        }
    }
}