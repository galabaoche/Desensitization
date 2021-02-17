using Desensitization.Desensitize.Extensions;
using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize
{
    public class DesensitizationMethod
    {
        public static string DesensitizeContact(AccountContactDto contactDto)
        {
            var defaultUserAuthorize =HttpContext.Current.GetDefaultUserAuthorize();

            if (!contactDto.IsForbidden && DesensitizationUtil.IsPhoneNumber(contactDto.Contact))
            {
                //屏蔽手机号
                if (!defaultUserAuthorize.DisplayPhone)
                {
                    contactDto.DesensitizeContact = DesensitizationUtil.FormatePhoneNumber(contactDto.Contact);
                }
            }

            //银行检查状态下所有的资料全部屏蔽，脱敏规则统一为中间4位以*号替换进行脱敏(需求1099)
            if (!defaultUserAuthorize.DisplayOtherContact && !DesensitizationUtil.IsPhoneNumber(contactDto.Contact))
            {
                contactDto.DesensitizeContact = DesensitizationUtil.TxtReplace(contactDto.Contact, 4, '*');
            }
            return contactDto.DesensitizeContact;
        }
    }
}