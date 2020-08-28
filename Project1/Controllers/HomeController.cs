﻿using HelloWorld.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace HelloWorld.Controllers
{
	public class HomeController : Controller
    {
        private IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ActionResult Register()
        {
            return View();
        }

        //TODO - DELETE
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //
        //[HttpPost]
        //public ActionResult Login(LoginModel loginModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Session["Email"] = loginModel.Email;
        //        return RedirectToAction("Classlist");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //Session[User] = User;
                System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.Email, loginModel.RememberMe);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else 
                {
                    return Redirect("~/");
                }               
            }
            else
            {
                return View(loginModel);
            }
        }

        public ActionResult LogOut()
        {
            //Session[User] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("~/");
        }  

        // GET: Home
        public ActionResult Index()
        {
                return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View(productRepository.Products.First());
        }

        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Products()
        {
            return View(productRepository.Products);
        }

        [Authorize]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Classlist()
        {
            return View(productRepository.Products);
        }
    }
}