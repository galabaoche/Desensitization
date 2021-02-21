using Oscar.Desensitization.Desensitize.Attributes;
using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize
{
    /// <summary>
    /// 脱敏规则的另一种配置方式
    /// </summary>
    public class DesensitizationRuleDictionary
    {
        public static IDictionary<string, List<DesensitizationAttribute>> Rules { get; private set; }
           = new Dictionary<string, List<DesensitizationAttribute>>(StringComparer.OrdinalIgnoreCase)
           {
               ["Desensitization.Dtos.AccountContactDto.DesensitizeContact"] =
                   new List<DesensitizationAttribute> {
                         new DesensitizationAttribute()
                         {
                            ShortDisplayName= "Phone",
                            //InlineConstraint= ":invoke(Oscar.Desensitization.Desensitize.DesensitizationUtil.IsPhoneNumber)&IsForbidden:equal(false)",
                            InlineConstraint= "IsForbidden:equal(false)",
                            Condition=_=>DesensitizationUtil.IsPhoneNumber(_),
                            CustomProcess=_=>DesensitizationUtil.FormatePhoneNumber(_),
                         },
                         new RangeMiddleHiddenAttribute(4)
                         {
                            ShortDisplayName= "NotPhone",
                            Condition=_=>!DesensitizationUtil.IsPhoneNumber(_),
                            //InlineConstraint= ":invoke(Oscar.Desensitization.Desensitize.DesensitizationUtil.IsNotPhoneNumber)",
                         },
                    }
           };
    }
}