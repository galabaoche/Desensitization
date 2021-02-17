using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// [Min,Max]位显示，其他隐藏
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeMiddleDisplayAttribute : DesensitizationAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public RangeMiddleDisplayAttribute(int min, int max) : this(string.Empty, min, max) { }
        public RangeMiddleDisplayAttribute(string ruleName, int min, int max):base(ruleName)
        {
            this.Min = min;
            this.Max = max;
        }

        public override string DesensitizateCore(string originVaule)
        {
            if (originVaule.Length < Min)
            {
                return originVaule;
            }
            if (originVaule.Length < Max)
            {
                Max = originVaule.Length;
            }

            var tempValue = string.Empty;
            if (Min > 1)
            {
                var needProcessValueLeft = originVaule.Substring(0, Min - 1);
                tempValue = originVaule.Replace(needProcessValueLeft, new string(DefaultDesensitizeChar, needProcessValueLeft.Length));
            }
            if (originVaule.Length > Max)
            {
                var needProcessValueRight = originVaule.Substring(Max, originVaule.Length - Max);
                tempValue = originVaule.Replace(needProcessValueRight, new string(DefaultDesensitizeChar, needProcessValueRight.Length));
            }

            return tempValue;
        }
    }
}