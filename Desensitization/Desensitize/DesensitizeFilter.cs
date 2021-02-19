using Desensitization.Desensitize.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize
{
    /// <summary>
    /// 脱敏filter，需要controller里返回json();
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DesensitizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var jsonResultl = filterContext.Result as JsonResult;
            if (jsonResultl != null)
            {
                jsonResultl.Data.Desensitizate();
            }
            base.OnActionExecuted(filterContext);
        }
    }
}