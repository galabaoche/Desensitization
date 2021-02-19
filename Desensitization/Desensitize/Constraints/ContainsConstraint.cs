using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证value里面是否包含此值
    /// </summary>
    public class ContainsConstraint : IConstraint
    {
        public ContainsConstraint(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            Value = value;
        }

        public object Value { get; private set; }
        public bool Match(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            string valueSettingString = Convert.ToString(Value, CultureInfo.InvariantCulture);
            if (value.GetType().IsValueType || Value.GetType().IsValueType)
            {
                return valueSettingString.ToLower().Contains(valueString.ToLower());
            }
            return valueSettingString.Contains(valueString);
        }
    }
}