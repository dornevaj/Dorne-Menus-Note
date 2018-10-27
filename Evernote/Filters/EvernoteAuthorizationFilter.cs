using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evernote.Filters
{
    public class EvernoteAuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["isLogin"] == null)
            {
                filterContext.Result = new RedirectToRouteResult("Login",null);
            } 
        }
    }
}