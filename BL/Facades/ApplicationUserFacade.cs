using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.Identity;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Riganti.Utils.Infrastructure.Core;
using BL.DTO.Filters;
using BL.Queries;
using BL.Repositories;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace BL.Facades
{
    public class ApplicationUserFacade : AppFacadeBase
    {
        public Func<ApplicationUserManager> UserManagerFactory { get; set; }
        


        public IdentityResult Register(ApplicationUserDTO user)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                IdentityResult result = null;

                using (var userManager = UserManagerFactory())
                {
                    
                    var appUser = Mapper.Map<ApplicationUser>(user);


                    appUser.UserName = appUser.Email;

                    Task.Run(async () =>
                    {

                        result = await userManager.CreateAsync(appUser, user.Password);

                    }).Wait();
                }

                uow.Commit();
                return result;
            }
        }


        public ClaimsIdentity Login(string email, string password)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var userManager = UserManagerFactory())
                {
                    ApplicationUser user;
                    try
                    {
                        user = userManager.Find(email, password);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
        }

        public void DeleteByEmail(string email)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var userManager = UserManagerFactory())
                {
                    var user = UserManagerFactory().FindByEmail(email);
                    if(user != null) UserManagerFactory().Delete(user);
                }
                uow.Commit();
            }
        }
    }
}
