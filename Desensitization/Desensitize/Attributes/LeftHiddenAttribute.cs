using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// 从左边开始隐藏Number位,其他显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LeftHiddenAttribute : DesensitizationAttribute
    {
        public int Number { get; set; }
        public LeftHiddenAttribute() { }
        public LeftHiddenAttribute(int number) : this(string.Empty, number) { }
        public LeftHiddenAttribute(string ruleName, int number):base(ruleName)
        {
            this.Number = number;
        }

        public override string DesensitizateCore(string originVaule)
        {
            if (originVaule.Length < Number)
            {
                Number = originVaule.Length;
            }
            var needProcessValue = originVaule.Substring(0, Number - 1);
            return originVaule.Replace(needProcessValue, new string(DefaultDesensitizeChar, needProcessValue.Length));
        }
    }
}