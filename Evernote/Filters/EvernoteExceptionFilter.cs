using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evernote.Filters
{
    public class EvernoteExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Controller.TempData["ExceptionError"] = filterContext.Exception;
            filterContext.Result = new RedirectToRouteResult("Error",null);
        }
        
    }
}