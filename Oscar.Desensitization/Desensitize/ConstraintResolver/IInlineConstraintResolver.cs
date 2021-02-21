using Oscar.Desensitization.Desensitize.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.Desensitization.Desensitize.ConstraintResolver
{
    /// <summary>
    /// 约束表达式解析接口
    /// </summary>
    public interface IInlineConstraintResolver
    {
        IConstraint ResolveConstraint(string inlineConstraint);
    }
}
