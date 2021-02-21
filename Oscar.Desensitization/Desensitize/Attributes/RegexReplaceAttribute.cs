using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Attributes
{

    /// <summary>
    /// 满足正则Pattern的会被替换为自定义的ReplaceContent
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RegexReplaceAttribute : DesensitizationAttribute
    {
        public string Pattern { get; set; }
        public RegexOptions RegexOptions { get; set; }
        public string ReplaceContent { get; set; }
        public RegexReplaceAttribute() { }
        public RegexReplaceAttribute(string pattern, string replaceContent, RegexOptions regexOptions = RegexOptions.Compiled)
            : this(string.Empty, pattern, replaceContent, regexOptions) { }
        public RegexReplaceAttribute(string ruleName, string pattern, string replaceContent, RegexOptions regexOptions = RegexOptions.Compiled) : base(ruleName)
        {
            this.Pattern = pattern;
            this.ReplaceContent = replaceContent;
            this.RegexOptions = regexOptions;
        }
        public override string DesensitizateCore(string originVaule)
        {
            Regex regex = new Regex(Pattern, RegexOptions);
            if (string.IsNullOrEmpty(ReplaceContent))
            {
                var matched = regex.Matches(originVaule);
                return regex.Replace(originVaule, new string(DefaultDesensitizeChar, matched[0].Length));
            }
            return regex.Replace(originVaule, ReplaceContent);
        }
    }
}