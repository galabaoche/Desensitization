using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.ConstraintResolver
{
    /// <summary>
    /// 什么也不做直接返回true，空对象模式
    /// </summary>
    public class NullConstraintMatchCheck : IConstraintMatchCheck
    {
        public bool Match()
        {
            return true;
        }
    }
}