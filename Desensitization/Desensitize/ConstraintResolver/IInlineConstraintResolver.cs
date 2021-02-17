using Desensitization.Desensitize.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desensitization.Desensitize.ConstraintResolver
{
    public interface IInlineConstraintResolver
    {
        IConstraint ResolveConstraint(string inlineConstraint);
    }
}
