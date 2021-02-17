using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// [1,Min][Max,Length]位隐藏，其他显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeSideHiddenAttribute : DesensitizationAttribute
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public RangeSideHiddenAttribute(int left, int right) : this(string.Empty, left, right) { }
        public RangeSideHiddenAttribute(string ruleName, int left, int right) : base(ruleName)
        {
            this.Left = left;
            this.Right = right;
        }

        public override string DesensitizateCore(string originVaule)
        {
            if (originVaule.Length < Left)
            {
                Left = originVaule.Length;
            }
            if (originVaule.Length < Right)
            {
                Right = originVaule.Length;
            }
            Left = Left < 1 ? 1 : Left;
            var needProcessValue1 = originVaule.Substring(0, Left - 1);
            var needProcessValue2 = originVaule.Substring(originVaule.Length - Right, originVaule.Length - 1);
            return originVaule.Replace(needProcessValue1, new string(DefaultDesensitizeChar, needProcessValue1.Length))
                .Replace(needProcessValue2, new string(DefaultDesensitizeChar, needProcessValue2.Length));
        }
    }
}