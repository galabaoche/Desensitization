using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证值是否大于等于Min
    /// </summary>
    public class MinConstraint : IConstraint
    {
        public MinConstraint(long min)
        {
            Min = min;
        }

        public long Min { get; private set; }

        public bool Match(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            long longValue;
            if (value is long)
            {
                longValue = (long)value;
                return longValue >= Min;
            }

            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            if (Int64.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out longValue))
            {
                return longValue >= Min;
            }
            return false;
        }
    }
}