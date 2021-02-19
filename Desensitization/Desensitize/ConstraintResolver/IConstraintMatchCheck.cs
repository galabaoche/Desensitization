using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.ConstraintResolver
{
    /// <summary>
    /// 约束检查接口
    /// </summary>
    public interface IConstraintMatchCheck
    {
        bool Match();
    }
}