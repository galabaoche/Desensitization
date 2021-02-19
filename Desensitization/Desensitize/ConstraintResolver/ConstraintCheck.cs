using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.ConstraintResolver
{
    /// <summary>
    /// 对外提供静态方法，直接调用此方法进行验证
    /// </summary>
    public class ConstraintCheck
    {
        public static bool Match(string template, ModelMetadata metadata)
        {
            if (string.IsNullOrEmpty(template))
            {
                return true;
            }
            var constraintMatchCheck = InlineTemplateParser.ParseTemplate(template, metadata);
            return constraintMatchCheck.Match();
        }
    }
}