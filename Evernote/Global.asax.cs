using Evernote.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Evernote
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SimpleInjectorInitializer.Initialize();//SimpleInjection IoC
        }

        /// <summary>
        /// no cache on browser back
        /// after log-out if user try go to the previous page dont let him without log-in
        /// </summary>
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }


        void Application_Error(object sender, EventArgs e)
        {
            // Grab information about the last error occurred 
            var exception = Server.GetLastError();

            // Clear the response stream 
            var httpContext = ((HttpApplication)sender).Context;
            httpContext.Response.Clear();
            httpContext.ClearError();
            httpContext.Response.TrySkipIisCustomErrors = true;

            // Manage to display a friendly view 
            var routeData = new RouteData();
            routeData.Values["controller"] = "error";
            routeData.Values["action"] = "applicationonerror";
            routeData.Values["exception"] = exception;
            using (var controller = new ErrorController())
            {
                ((IController)controller).Execute(
                new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }
        
    }
}
