using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// [Left,Right]位显示，其他隐藏
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeMiddleDisplayAttribute : DesensitizationAttribute
    {
        public int? Left { get; set; }
        public int? Right { get; set; }
        public int? Number { get; set; }
        public RangeMiddleDisplayAttribute() { }
        public RangeMiddleDisplayAttribute(int number) : this(string.Empty, number) { }
        public RangeMiddleDisplayAttribute(string ruleName, int number) : base(ruleName)
        {
            this.Number = number;
        }
        public RangeMiddleDisplayAttribute(int left, int right) : this(string.Empty, left, right) { }
        public RangeMiddleDisplayAttribute(string ruleName, int left, int right) : base(ruleName)
        {
            this.Left = left;
            this.Right = right;
        }

        public override string DesensitizateCore(string originVaule)
        {
            if (Number.HasValue)
            {
                if (Number.Value >= originVaule.Length)
                {
                    return originVaule;
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

            var tempValue = string.Empty;
            if (Left > 1)
            {
                var needProcessValueLeft = originVaule.Substring(0, Left.Value - 1);
                tempValue = originVaule.Replace(needProcessValueLeft, new string(DefaultDesensitizeChar, needProcessValueLeft.Length));
            }
            if (originVaule.Length > Right)
            {
                var needProcessValueRight = originVaule.Substring(Right.Value, originVaule.Length - Right.Value);
                tempValue = originVaule.Replace(needProcessValueRight, new string(DefaultDesensitizeChar, needProcessValueRight.Length));
            }

            return tempValue;
        }
    }
}