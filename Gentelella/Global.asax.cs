using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using Gentelella.Interface;
using Gentelella.Service;

namespace Gentelella
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            // Register the MVC controllers.
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UserLogic>().As<IUserLogic>();

            builder.RegisterFilterProvider();

            // Build the container.
            var container = builder.Build();

            // Configure MVC with the dependency resolver.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // Configure MVC move the version header
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}
