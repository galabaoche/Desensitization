using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    public interface IConstraint
    {
        bool Match(object value);
    }
}