using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Desensitization.Desensitize.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RegexInsertAttribute : DesensitizationAttribute
    {
        public string Pattern { get; set; }
        public RegexOptions RegexOptions { get; set; }
        public string ReplaceContent { get; set; }
        public RegexInsertAttribute(string pattern, string replaceContent, RegexOptions regexOptions = RegexOptions.Compiled)
           : this(string.Empty, pattern, replaceContent, regexOptions) { }
        public RegexInsertAttribute(string  ruleName, string pattern, string replaceContent, RegexOptions regexOptions = RegexOptions.Compiled) :base(ruleName)
        {
            this.Pattern = pattern;
            this.ReplaceContent = replaceContent;
            this.RegexOptions = regexOptions;
        }
        public override string DesensitizateCore(string originVaule)
        {
            Regex regex = new Regex(Pattern, RegexOptions);
            return regex.Replace(originVaule, "$1" + ReplaceContent);
        }
    }
}