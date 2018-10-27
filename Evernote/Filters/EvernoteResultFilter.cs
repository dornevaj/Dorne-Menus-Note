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
    public class EvernoteResultFilter : FilterAttribute, IResultFilter
    {
        private readonly IFilterLogService _filterLogService;
        public EvernoteResultFilter(IFilterLogService filterLogService)
        {
            _filterLogService = filterLogService;
        }

        //After Page Created View
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            FilterLog filterLog = new FilterLog()
            {
                ControllerName = filterContext.RouteData.Values["action"].ToString(),
                ActionName = filterContext.RouteData.Values["controller"].ToString(),
                Date = DateTime.Now,
                UserId = (filterContext.HttpContext.Session["isLogin"] as User).UserId,
                Description = "OnResultExecuted"
            };

            _filterLogService.Add(filterLog);
        }

        //Before Go To View
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            FilterLog filterLog = new FilterLog()
            {
                ControllerName = filterContext.RouteData.Values["action"].ToString(),
                ActionName = filterContext.RouteData.Values["controller"].ToString(),
                Date = DateTime.Now,
                UserId = (filterContext.HttpContext.Session["isLogin"] as User).UserId,
                Description = "OnResultExecuting"
            };

            _filterLogService.Add(filterLog);
        }
    }
}