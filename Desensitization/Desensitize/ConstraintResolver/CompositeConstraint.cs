using Desensitization.Desensitize.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.ConstraintResolver
{

    public class CompositeConstraint : IConstraint
    {
        public CompositeConstraint(IList<IConstraint> constraints)
        {
            if (constraints == null)
            {
                throw new ArgumentNullException("constraints");
            }

            Constraints = constraints;
        }

        public IEnumerable<IConstraint> Constraints { get; private set; }

        public bool Match(object value)
        {
            foreach (var constraint in Constraints)
            {
                if (!constraint.Match(value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}