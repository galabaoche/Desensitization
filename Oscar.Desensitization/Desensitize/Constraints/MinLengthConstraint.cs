using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证长度是否大于等于MinLength
    /// </summary>
    public class MinLengthConstraint : IConstraint
    {
        public MinLengthConstraint(int minLength)
        {
            if (minLength < 0)
            {
                throw new ArgumentOutOfRangeException("minLength", minLength, "minLength not allow less than 0");
            }

            MinLength = minLength;
        }

        public int MinLength { get; private set; }

        public bool Match(object value)
        {

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            return valueString.Length >= MinLength;
        }
    }
}