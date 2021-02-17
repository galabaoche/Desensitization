using Desensitization.Desensitize.ConstraintResolver;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Desensitization.Desensitize.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomRuleAttribute : Attribute, IMetadataAware
    {
        private object _typeId;
        public override object TypeId
        {
            get { return _typeId ?? (_typeId = new object()); }
        }
        public CustomRuleAttribute(string ruleName, string inlineConstraint)
        {
            this.RuleName = ruleName;
            this.InlineConstraint = inlineConstraint;
        }
        public string RuleName { get; set; }
        public string InlineConstraint { get; set; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            var model = metadata.Model;
            if (model == null)
            {
                return;
            }
            if (ConstraintCheck.Match(InlineConstraint,metadata))
            {
                metadata.Description = RuleName;
            }
        }
    }
}