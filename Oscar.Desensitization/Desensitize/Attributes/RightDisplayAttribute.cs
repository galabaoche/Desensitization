using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// 从右边开始显示Number位,其他隐藏
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RightDisplayAttribute : DesensitizationAttribute
    {
        public int Number { get; set; }
        public RightDisplayAttribute() { }
        public RightDisplayAttribute(int number) : this( string.Empty, number) { }
        public RightDisplayAttribute(string ruleName, int number) :base(ruleName)
        {
            this.Number = number;
        }

        public override string DesensitizateCore(string originVaule)
        {
            if (originVaule.Length < Number)
            {
                return originVaule;
            }

            var needProcessValue = originVaule.Substring(0, originVaule.Length - Number);
            return originVaule.Replace(needProcessValue, new string(DefaultDesensitizeChar, needProcessValue.Length));
        }
    }
}