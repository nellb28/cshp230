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


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                //userModel.Email =  
                //userModel.Email = userRepository.

                return RedirectToAction("Classlist");
            }
            else
            {
                return View();
            }
        }


        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //Session[User] = User;
                var user = userRepository.LogIn(loginModel.Email, loginModel.Password);
                //var temp = userRepository.Register(loginModel.Email, loginModel.Password);
                //userRepository.t
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

        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Classlist()
        {
            return View(productRepository.Products);
        }

        [Authorize]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult EnrollInClass()
        {
            return View(productRepository.Products);
        }
    }
}