using Desensitization.Desensitize;
using Desensitization.Desensitize.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Controllers
{
    public class ControllerBase : Controller
    {
        protected internal DesensitizeResult Desensitizate(object data)
        {
            data.Desensitizate();
            return new DesensitizeResult(data);
        }
    }
}