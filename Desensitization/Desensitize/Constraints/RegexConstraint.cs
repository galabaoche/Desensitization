using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 验证值是否匹配指定的Pattern正则
    /// </summary>
    public class RegexConstraint : IConstraint
    {
        private readonly Regex _regex;

        public RegexConstraint(string pattern)
        {
            Pattern = pattern;
            _regex = new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public string Pattern { get; private set; }

        public bool Match(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            return _regex.IsMatch(valueString);
        }
    }
}