using Desensitization.Desensitize.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.ConstraintResolver
{
    /// <summary>
    ///  多个约束采用并且的关系做验证
    /// </summary>
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