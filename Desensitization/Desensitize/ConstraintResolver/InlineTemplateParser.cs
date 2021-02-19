using Desensitization.Desensitize.Constraints;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.ConstraintResolver
{
    /// <summary>
    /// 内联约束检查解析器
    /// </summary>
    public class InlineTemplateParser
    {
        public static IConstraintMatchCheck ParseTemplate(string template, ModelMetadata metadata)
        {
            if (string.IsNullOrEmpty(template))
            {
                return new NullConstraintMatchCheck();
            }
            IDictionary<IConstraint, object> constraints = new Dictionary<IConstraint, object>();

            var andConstraints = template.Split('&');
            if (andConstraints.Length > 1)
            {
                foreach (var segmentConstraint in andConstraints)
                {
                    ParseSegment(segmentConstraint, metadata, constraints);
                }
                return new AndConstraintMatchCheck(constraints);
            }

            ParseSegment(template, metadata, constraints);
            return new AndConstraintMatchCheck(constraints); ;
        }

        private static void ParseSegment(string inlineConstraint, ModelMetadata metadata, IDictionary<IConstraint, object> constraints)
        {
            var model = metadata.Model;
            string[] split = inlineConstraint.Split(':');
            string propertyName = split[0];
            object value = model;
            if (!string.IsNullOrEmpty(propertyName) && metadata.PropertyName != propertyName)
            {
                ModelMetadata propertyMetadata = null;
                if (metadata.Container == null)
                {
                    propertyMetadata = metadata.Properties.FirstOrDefault(p => p.PropertyName == propertyName);
                    if (propertyMetadata == null)
                    {
                        return;
                    }
                    value = propertyMetadata.Model;
                }
                else
                {
                    //防止循环中频繁反射损伤性能，使用表达式树做了编译缓存
                    PropertyExecutor executor = new PropertyExecutor(metadata.ContainerType.FullName, propertyName);
                    value = executor.GetValue(metadata.Container);
                    //var propertyInfo= metadata.ContainerType.GetProperty(propertyName);
                    //if (propertyInfo==null)
                    //{
                    //    return;
                    //}
                    //value = propertyInfo.GetValue(metadata.Container);
                }
            }

            DefaultInlineConstraintResolver defaultInlineConstraint = new DefaultInlineConstraintResolver();
            IList<IConstraint> constraintList = new List<IConstraint>();
            for (int i = 1; i < split.Length; i++)
            {
                var constraint = defaultInlineConstraint.ResolveConstraint(split[i]);
                if (constraint != null)
                {
                    constraintList.Add(constraint);
                }
            }

            constraints.Add(new CompositeConstraint(constraintList), value);
        }
    }
}