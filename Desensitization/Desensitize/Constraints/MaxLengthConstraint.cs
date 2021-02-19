using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证长度是否小于等于MaxLength
    /// </summary>
    public class MaxLengthConstraint : IConstraint
    {
        public MaxLengthConstraint(int maxLength)
        {
            if (maxLength < 0)
            {
                throw new ArgumentOutOfRangeException("maxLength", maxLength, "maxLength not allow less than 0");
            }
            MaxLength = maxLength;
        }

        public int MaxLength { get; private set; }

        public bool Match(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            return valueString.Length <= MaxLength;
        }
    }
}