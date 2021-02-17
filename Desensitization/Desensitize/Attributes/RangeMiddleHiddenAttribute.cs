using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// [Min,Max]位隐藏，其他显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeMiddleHiddenAttribute : DesensitizationAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public Func<string,int> MinFactory { get; set; }
        public Func<string, int> MaxFactory { get; set; }
        public RangeMiddleHiddenAttribute(int min, int max) : this(string.Empty, min, max) { }
        public RangeMiddleHiddenAttribute(Func<string, int> minFactory, Func<string, int> maxFactory)
            : this(string.Empty, minFactory, maxFactory) { }
        public RangeMiddleHiddenAttribute(string ruleName, int min, int max):base(ruleName)
        {
            this.Min = min;
            this.Max = max;
        }
        public RangeMiddleHiddenAttribute(string ruleName, Func<string, int> minFactory, Func<string, int> maxFactory) : base(ruleName)
        {
            this.MinFactory = minFactory;
            this.MaxFactory = maxFactory;
        }
        public override string DesensitizateCore(string originVaule)
        {
            if (MinFactory != null && MaxFactory != null)
            {
                this.Min = MinFactory(originVaule);
                this.Max = MaxFactory(originVaule);
            }
            if (originVaule.Length < Min)
            {
                return originVaule;
            }
            if (originVaule.Length < Max)
            {
                Max = originVaule.Length;
            }
            var tempValue = originVaule;
            Min = Min < 1 ? 1 : Min;
            if (Max > Min)
            {
                var needProcessValue = originVaule.Substring(Min - 1, Max - Min + 1);
                tempValue = originVaule.Replace(needProcessValue, new string(DefaultDesensitizeChar, needProcessValue.Length));
            }
            return tempValue;
        }
    }
}
