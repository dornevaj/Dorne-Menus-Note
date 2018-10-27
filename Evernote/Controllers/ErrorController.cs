using Evernote.Filters;
using System;
using System.Web.Mvc;

namespace Evernote.Controllers
{
    [EvernoteExceptionFilter,EvernoteAuthorizationFilter]
    public class ErrorController : Controller
    {

        /// <summary>
        /// This Action Work With Filter->EvernoteExceptionFilter
        /// Capture Er
        /// </summary>
        /// <returns>Display Error And Error Message</returns>
        public ActionResult Error()
        {
            if (TempData["ExceptionError"] == null)
            {
                return View("Index","Home");
            }
            else
            {
                Exception exceptionModel = TempData["ExceptionError"] as Exception;
                return View(exceptionModel);
            }
        }

        //Global Asax ->  Application_Error
        public ActionResult ApplicationOnError(Exception exception)
        {
            
            return View(exception);
        }
    }
}