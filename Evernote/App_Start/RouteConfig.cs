using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Evernote
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Login",
                "Login",
                new { Controller = "Account", Action = "Login" }
            );

            routes.MapRoute(
               "Error",
               "Error",
               new { Controller = "Error", Action = "Error" }
           );

            routes.MapRoute(
              "Logout",
              "Logout",
              new { Controller = "Account", Action = "Logout" }
          );

            routes.MapRoute(
                "Register",
                "Register",
                new { Controller = "Account", Action = "Register" }
            );

            routes.MapRoute(
               "RecoveryPassword",
               "RecoveryPassword",
               new { Controller = "Account", Action = "RecoveryPassword" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
