using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookLoansPorject.ViewModels;
using Microsoft.Owin.Security;

namespace BookLoansPorject.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(model.Username, model.Password);

            if (user != null)
            {
                var umanager = HttpContext.GetOwinContext().Authentication;
                var ui = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                umanager.SignIn(new AuthenticationProperties() { IsPersistent = false }, ui);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var store = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(store);
            var user = new IdentityUser { UserName = model.Username };
            IdentityResult result = manager.Create(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                ModelState.AddModelError("", "Registration failed");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            var umanager = HttpContext.GetOwinContext().Authentication;
            umanager.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
    }
}