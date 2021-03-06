﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证值是否等于value
    /// </summary>
    public class EqualConstraint : IConstraint
    {
        public EqualConstraint(object value)
        {
            Value = value;
        }

        public object Value { get; private set; }
        public bool Match(object value)
        {
            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            string valueSettingString = Convert.ToString(Value, CultureInfo.InvariantCulture);
            if (value.GetType().IsValueType || Value.GetType().IsValueType)
            {
                return valueString?.ToLower() == valueSettingString?.ToLower();
            }
            return valueString == valueSettingString;
        }
    }
}