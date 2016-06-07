using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Microsoft.Owin.Security;
using WebGrease.Css.Extensions;
using AutoMapper;
using System;

namespace WebApp.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ApplicationUserFacade userFacade;

        private readonly PersonFacade _personFacade;
        

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public LoginController(ApplicationUserFacade userFacade, PersonFacade personFacade)
        {
            this.userFacade = userFacade;
            _personFacade = personFacade;
        }
        

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ApplicationUserDTO user)
        {
            var blPerson = Mapper.Map<PersonDTO>(user);
            var result = userFacade.Register(user);
            if (!result.Succeeded)
            {
                result.Errors.ForEach(x => ModelState.AddModelError(string.Empty, x));
                return View();
            }
            _personFacade.CreatePerson(blPerson);
            return RedirectToAction("Login");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ApplicationUserDTO user)
        {
            System.Security.Claims.ClaimsIdentity identity;
            try
            {
                identity = userFacade.Login(user.Email, user.Password);
            } catch (ArgumentNullException ex)
            {
                ModelState.AddModelError("LogOnError", "The username or password is incorrect.");
                return RedirectToAction("Login");
            }


            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            

            return RedirectToAction("Projects", "Project");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(new AuthenticationProperties() { IsPersistent = false });

            return RedirectToAction("Index", "Home");
        }
    }
}