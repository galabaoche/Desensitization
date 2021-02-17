using Desensitization.Desensitize.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Dtos
{
    //[CustomMethod("Desensitization.Desensitize.DesensitizationMethod.DesensitizeContact")]
    [CustomRule(DesensitizionKey.XYPH, "CustomerId:equal(930679933297131520)")]
    [CustomRule(DesensitizionKey.PF, "CustomerId:equal(67)")]
    public class AccountContactDto
    {
        public string FactTypeName { get; set; }

        [LeftDisplay(DesensitizionKey.PF, 1)]
        public string ContactName { get; set; }
        public string Contact { get; set; }

        [RightHidden(DesensitizionKey.XYPH, 4, ShortDisplayName = "身份证地址", InlineConstraint = "ContactTypeName:equal(身份证地址)")]
        [RightDisplay(DesensitizionKey.PF, 2, ShortDisplayName = "固话", InlineConstraint = "FactTypeName:equal(固话)")]
        [RightDisplay(DesensitizionKey.PF, 4, ShortDisplayName = "手机", InlineConstraint = "FactTypeName:equal(手机)")]
        [RightHidden(DesensitizionKey.PF, 6, ShortDisplayName = "其他", InlineConstraint = "FactTypeName:contains(地址,单位名称,其他)")]
        [RightHidden(DesensitizionKey.PF, 4, ShortDisplayName = "微信", InlineConstraint = ":maxlength(8)&FactTypeName:equal(微信)")]
        [RightHidden(DesensitizionKey.PF, 6, ShortDisplayName = "微信", InlineConstraint = ":length(9,11)&FactTypeName:equal(微信)")]
        [RightHidden(DesensitizionKey.PF, 8, ShortDisplayName = "微信", InlineConstraint = ":minlength(12)&FactTypeName:equal(微信)")]
        [RegexInsert(DesensitizionKey.PF, @"(^.+?)(?=@.*$)", "***", ShortDisplayName = "Email", InlineConstraint = ":regex(^.{0,3}@.+$)&FactTypeName:equal(Email)")]
        [RegexReplace(DesensitizionKey.PF, @"(?<=^.{3})(.+?)(?=@.*$)", "***", ShortDisplayName = "Email", InlineConstraint = ":regex(^.{4,}@.+$)&FactTypeName:equal(Email)")]
        public string DesensitizeContact { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactTypeName { get; set; }
        public bool IsForbidden { get; set; }
    }
}