using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oscar.Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// 脱敏规则父类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DesensitizationAttribute : Attribute
    {
        private object _typeId;
        public override object TypeId
        {
            get { return _typeId ?? (_typeId = new object()); }
        }

        public DesensitizationAttribute() { }
        public DesensitizationAttribute(string ruleName)
        {
            RuleName = ruleName;
        }

        /// <summary>
        /// 优先级，值越小优先级越高
        /// </summary>
        public int Order { get; set; } = 10000;

        /// <summary>
        /// 脱敏约束
        /// </summary>
        public string InlineConstraint { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool ShowForDisplay { get; set; }

        /// <summary>
        /// 简短显示名称
        /// </summary>
        public string ShortDisplayName { get; set; }

        /// <summary>
        /// 脱敏条件
        /// </summary>
        public Func<string, bool> Condition { get; set; }

        /// <summary>
        /// 自定义脱敏处理
        /// </summary>
        public Func<string, string> CustomProcess { get; set; }

        /// <summary>
        /// 规则名
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// 脱敏默认替换符号
        /// </summary>
        public char DefaultDesensitizeChar { get; set; } = '*';

        /// <summary>
        /// 脱敏
        /// </summary>
        /// <param name="originVaule"></param>
        /// <returns></returns>
        public string Desensitizate(string originVaule)
        {
            if (CustomProcess != null)
            {
                return CustomProcess(originVaule);
            }
            return DesensitizateCore(originVaule);
        }

        public virtual string DesensitizateCore(string originVaule)
        {
            return originVaule;
        }
    }
}