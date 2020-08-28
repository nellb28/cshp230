using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

//TODO - use this to fix login issues
namespace HelloWorld.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.LogIn(model.Email, model.Password);
                if (user != null)
                {
                    Session["User"] = user;
                    System.Web.Security.FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");

            }

            return View(model);
        }
    }
}