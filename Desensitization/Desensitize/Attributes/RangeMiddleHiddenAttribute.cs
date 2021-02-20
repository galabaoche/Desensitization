using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// [Left,Right]位隐藏，其他显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeMiddleHiddenAttribute : DesensitizationAttribute
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public Func<string,int> LeftFactory { get; set; }
        public Func<string, int> RightFactory { get; set; }
        public RangeMiddleHiddenAttribute() { }
        public RangeMiddleHiddenAttribute(int left, int right) : this(string.Empty, left, right) { }
        public RangeMiddleHiddenAttribute(Func<string, int> LeftFactory, Func<string, int> RightFactory)
            : this(string.Empty, LeftFactory, RightFactory) { }
        public RangeMiddleHiddenAttribute(string ruleName, int left, int right):base(ruleName)
        {
            this.Left = left;
            this.Right = right;
        }
        public RangeMiddleHiddenAttribute(string ruleName, Func<string, int> LeftFactory, Func<string, int> RightFactory) : base(ruleName)
        {
            this.LeftFactory = LeftFactory;
            this.RightFactory = RightFactory;
        }
        public override string DesensitizateCore(string originVaule)
        {
            if (LeftFactory != null && RightFactory != null)
            {
                this.Left = LeftFactory(originVaule);
                this.Right = RightFactory(originVaule);
            }
            if (originVaule.Length < Left)
            {
                return originVaule;
            }
            if (originVaule.Length < Right)
            {
                Right = originVaule.Length;
            }
            var tempValue = originVaule;
            Left = Left < 1 ? 1 : Left;
            if (Right > Left)
            {
                var needProcessValue = originVaule.Substring(Left - 1, Right - Left + 1);
                tempValue = originVaule.Replace(needProcessValue, new string(DefaultDesensitizeChar, needProcessValue.Length));
            }
            return tempValue;
        }
    }
}
