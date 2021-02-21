using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证值是否在Min与Max之间
    /// </summary>
    public class RangeConstraint: IConstraint
    {
        public RangeConstraint(long min, long max)
        {
            Min = min;
            Max = max;
        }

        public long Min { get; private set; }

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
                return longValue >= Min && longValue <= Max;
            }

            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            if (Int64.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out longValue))
            {
                return longValue >= Min && longValue <= Max;
            }
            return false;
        }
    }
}