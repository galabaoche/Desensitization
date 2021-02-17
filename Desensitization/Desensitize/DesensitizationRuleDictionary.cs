using Desensitization.Desensitize.Attributes;
using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize
{
    public class DesensitizationRuleDictionary
    {
        public static IDictionary<string, List<DesensitizationAttribute>> Rules { get; private set; }
           = new Dictionary<string, List<DesensitizationAttribute>>(StringComparer.OrdinalIgnoreCase)
           {
               ["Desensitization.Dtos.AccountDto.AccountNumber"] = new List<DesensitizationAttribute> {
                   new RangeMiddleHiddenAttribute(_ => _.Length / 2 - 1, _ => _.Length / 2 + 2),
               },
               ["Desensitization.Dtos.AccountContactDto.DesensitizeContact"] =
                   new List<DesensitizationAttribute> {
                         new DesensitizationAttribute()
                         {
                            ShortDisplayName= "Phone",
                            //InlineConstraint= ":invoke(Desensitization.Desensitize.DesensitizationUtil.IsPhoneNumber)&IsForbidden:equal(false)",
                            InlineConstraint= "IsForbidden:equal(false)",
                            Condition=_=>DesensitizationUtil.IsPhoneNumber(_),
                            CustomProcess=_=>DesensitizationUtil.FormatePhoneNumber(_),
                         },
                         new RangeMiddleHiddenAttribute(_ => _.Length / 2 - 1, _ => _.Length / 2 + 2)
                         {
                            ShortDisplayName= "NotPhone",
                            Condition=_=>!DesensitizationUtil.IsPhoneNumber(_),
                            //InlineConstraint= ":invoke(Desensitization.Desensitize.DesensitizationUtil.IsNotPhoneNumber)",
                         },
                    }
           };
    }
}