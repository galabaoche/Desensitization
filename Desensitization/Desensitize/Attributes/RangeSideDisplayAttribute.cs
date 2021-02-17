using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// [1,Min][Max,Length]位显示，其他隐藏
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeSideDisplayAttribute : DesensitizationAttribute
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public RangeSideDisplayAttribute(int left, int right) : this(string.Empty, left, right) {}
        public RangeSideDisplayAttribute(string ruleName, int left, int right) : base(ruleName)
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
            if (originVaule.Length - Right - Left > 0)
            {
                var needProcessValue = originVaule.Substring(Left, originVaule.Length - Right - Left);
                return originVaule.Replace(needProcessValue, new string(DefaultDesensitizeChar, needProcessValue.Length));
            }
            return originVaule;
        }
    }
}