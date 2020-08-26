using System.Web;
using System.Web.Mvc;

namespace HelloWorld
{
    public class AuthorizeIPAddressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequest = HttpContext.Current.Request;
            //var currentRequest = filterContext.HttpContext.Request;

            var clientIPAddress = currentRequest.UserHostAddress;

            if (clientIPAddress == "::1" || clientIPAddress == "127.0.0.1")
            {
                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}