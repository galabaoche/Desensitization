using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证值是否小于等于Max
    /// </summary>
    public class MaxConstraint : IConstraint
    {
        public MaxConstraint(long max)
        {
            Max = max;
        }

        public long Max { get; private set; }


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
                return longValue <= Max;
            }

            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            if (Int64.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out longValue))
            {
                return longValue <= Max;
            }
            return false;
        }
    }
}