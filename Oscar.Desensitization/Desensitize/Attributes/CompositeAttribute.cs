using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CompositeAttribute : DesensitizationAttribute
    {
        public List<DesensitizationAttribute> Attributes { get; set; }
        public CompositeAttribute(List<DesensitizationAttribute> attributes)
            : this(string.Empty, attributes) { }
        public CompositeAttribute(string ruleName, List<DesensitizationAttribute> attributes) : base(ruleName)
        {
            this.Attributes = attributes ?? throw new NullReferenceException("attributes");
        }
        public override string DesensitizateCore(string originVaule)
        {
            if (Attributes != null && Attributes.Count() > 0)
            {
                Attributes = Attributes.OrderBy(a => a.Order).ToList();
                foreach (var attribute in Attributes)
                {
                    if (attribute.Condition(originVaule))
                    {
                        return attribute.Desensitizate(originVaule);
                    }
                }
            }
            return originVaule;
        }
    }
}