using Desensitization.Desensitize.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.ConstraintResolver
{
    public class AndConstraintMatchCheck : IConstraintMatchCheck
    {
        public AndConstraintMatchCheck(IDictionary<IConstraint, object> constraints)
        {
            if (constraints == null)
            {
                throw new ArgumentNullException("constraints");
            }

            Constraints = constraints;
        }

        public IDictionary<IConstraint, object> Constraints { get; private set; }

        public bool Match()
        {
            foreach (var constraint in Constraints)
            {
                if (!constraint.Key.Match(constraint.Value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}