using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sofia.Web.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //File.AppendAllText(@"C:\Temp\Log.txt", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "::" + filterContext.ActionDescriptor.ActionName + Environment.NewLine);
        }
    }
}