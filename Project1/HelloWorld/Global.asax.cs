using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWorld
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterAutofac();
        }

        private void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();

            //builder.RegisterType<ContactRepository>().As<IContactRepository>();

            var container = builder.Build();

            // Configure dependency resolver.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            //Server.ClearError();

            //var routeData = new RouteData();
            //routeData.Values.Add("controller", "Error");
            //routeData.Values.Add("action", "Error");

            //IController errorController = new Controllers.ErrorController();
            //errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

            // OR
            //Context.Server.TransferRequest("/error/error");
            // OR
            //Context.Response.Redirect("/error/error");
        }
    }
}
