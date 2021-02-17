using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.ConstraintResolver
{
    public class NullConstraintMatchCheck : IConstraintMatchCheck
    {
        public bool Match()
        {
            return true;
        }
    }
}