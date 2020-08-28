using HelloWorld.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace HelloWorld.Controllers
{
	public class HomeController : Controller
    {
        private IProductRepository productRepository;
        private IUserRepository userRepository;

        public HomeController(IProductRepository productRepository, IUserRepository userRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        //private IUserRepository userRepository;
        //
        //public HomeController(IUserRepository userRepository)
        //{
        //    this.userRepository = userRepository;
        //}
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
                //TODO - Refactor this!
                //Session[User] = User;
                var user = userRepository.LogIn(loginModel.Email, loginModel.Password);
                if (user != null)
                {
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
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(loginModel);
           
        }
        //[HttpPost]
        //public ActionResult LogOn(LogOnModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = userRepository.LogIn(model.Email, model.Password);
        //        if (user != null)
        //        {
        //            Session["User"] = user;
        //            System.Web.Security.FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
        //            return Redirect(returnUrl);
        //        }
        //
        //        ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //
        //    }
        //
        //    return View(model);
        //}

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