using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// [Left,Right]位隐藏，其他显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeMiddleHiddenAttribute : DesensitizationAttribute
    {
        public int? Left { get; set; }
        public int? Right { get; set; }
        public int? Number { get; set; }
        public RangeMiddleHiddenAttribute() { }
        public RangeMiddleHiddenAttribute(int left, int right) : this(string.Empty, left, right) { }
        public RangeMiddleHiddenAttribute(string ruleName, int left, int right):base(ruleName)
        {
            this.Left = left;
            this.Right = right;
        }
        public RangeMiddleHiddenAttribute(int number) : this(string.Empty, number) { }
        public RangeMiddleHiddenAttribute(string ruleName, int number) : base(ruleName)
        {
            this.Number = number;
        }
        public override string DesensitizateCore(string originVaule)
        {
            if (Number.HasValue)
            {
                if (Number.Value >=originVaule.Length)
                {
                    this.Left = 1;
                    this.Right = originVaule.Length;
                }
                else
                {
                    Left = originVaule.Length / 2 - Number / 2 + 1;
                    Right = originVaule.Length / 2 + Number / 2;
                }
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
                var needProcessValue = originVaule.Substring(Left.Value - 1, Right.Value - Left.Value + 1);
                tempValue = originVaule.Replace(needProcessValue, new string(DefaultDesensitizeChar, needProcessValue.Length));
            }
            return tempValue;
        }
    }
}
