using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 约束接口
    /// </summary>
    public interface IConstraint
    {
        bool Match(object value);
    }
}