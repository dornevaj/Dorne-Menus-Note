using Evernote.BLL.Abstract;
using Evernote.Entities;
using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evernote.Filters
{
    public class EvernoteActionFilter : FilterAttribute, IActionFilter
    {
        private readonly IFilterLogService _filterLogService;

        public EvernoteActionFilter(IFilterLogService filterLogService)
        {
            _filterLogService = filterLogService;
        }

        //After Action Executed
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            FilterLog filterLog = new FilterLog()
            {
                ActionName = filterContext.ActionDescriptor.ActionName,
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Date = DateTime.Now,
                UserId = (filterContext.HttpContext.Session["isLogin"] as User).UserId,
                Description = "OnActionExecuted"
            };

            _filterLogService.Add(filterLog);
        }

        //Before Action Executed
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Get Controller
            //var controllerUseThisAttribute =((Controllers.HomeController) filterContext.Controller);
            //var controllerUseThisAttribute2 =(filterContext.Controller);

            FilterLog filterLog = new FilterLog()
            {
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                ActionName = filterContext.ActionDescriptor.ActionName,
                Date = DateTime.Now,
                UserId = (filterContext.HttpContext.Session["isLogin"] as User).UserId,
                Description = "OnActionExecuting"
            };

            _filterLogService.Add(filterLog);
        }
    }
}