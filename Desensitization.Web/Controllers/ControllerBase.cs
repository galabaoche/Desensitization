using Desensitization.Web.Extensions;
using Oscar.Desensitization.Desensitize.Extensions;
using System.Web.Mvc;

namespace Desensitization.Web.Controllers
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