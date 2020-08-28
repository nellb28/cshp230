using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }
    }
}