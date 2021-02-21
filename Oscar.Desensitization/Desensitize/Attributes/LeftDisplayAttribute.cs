using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// 从左边开始显示Number位,其他隐藏
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LeftDisplayAttribute : DesensitizationAttribute
    {
        public int Number { get; set; }
        public Func<string, int> NumberFactory { get; set; }
        public LeftDisplayAttribute() { }
        public LeftDisplayAttribute(int number) : this(string.Empty, number) { }
        public LeftDisplayAttribute(string ruleName, int number):base(ruleName)
        {
            this.Number = number;
        }
        public LeftDisplayAttribute(Func<string, bool> condition, Func<string, int> numberFactory)
        {
            this.NumberFactory = numberFactory;
            Condition = condition;
        }
        public override string DesensitizateCore(string originVaule)
        {
            if (NumberFactory != null)
            {
                this.Number = NumberFactory(originVaule);
            }
            if (originVaule.Length <= Number)
            {
                return originVaule;
            }
            var needProcessValue = originVaule.Substring(Number, originVaule.Length - Number);
            return originVaule.Replace(needProcessValue, new string(DefaultDesensitizeChar, needProcessValue.Length));
        }
    }
}