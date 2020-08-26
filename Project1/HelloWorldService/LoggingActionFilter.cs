using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace HelloWorldService
{
    public class LoggingActionFilter : IActionFilter
    {
        private System.Diagnostics.Stopwatch stopwatch;

        private IHostingEnvironment env;

        public LoggingActionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            stopwatch.Stop();

            var webroot = env.WebRootPath;
            var filepath = Path.Combine(webroot, "logger.txt");
            
            var controllerName = actionExecutedContext.Controller.ToString();
            var methodName = ((ControllerBase)actionExecutedContext.Controller).Request.Method;

            var logline = string.Format("{0} : {1} {2} Elapsed={3}\n", System.DateTime.Now, controllerName, methodName, stopwatch.Elapsed);

            File.AppendAllText(filepath, logline);
        }
    }
}
