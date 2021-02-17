using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Permissions
{
    public class DesensitizationPermissionDictionary
    {
        public static IDictionary<string, Func<PFUserAuthorize, bool>> PFUserAuthorizeDictionary { get; private set; }
             = new Dictionary<string, Func<PFUserAuthorize, bool>>(StringComparer.OrdinalIgnoreCase)
             {
                 ["Desensitization.Dtos.AccountDto.OutIDNumber"] = _ => _.DisplayCard,
                 ["Desensitization.Dtos.DebtorDto.IDNumber"] = _ => _.DisplayIdNumber,
                 ["Desensitization.Dtos.DebtorDto.DebtorName"] = _ => _.DisplayDebtorName,
                 ["Desensitization.Dtos.AccountContactDto.ContactName"] = _ => _.DisplayDebtorName,
                 ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.固话"] = _ => _.DisplayPhone,
                 ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.手机"] = _ => _.DisplayPhone,
                 ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.微信"] = _ => _.DisplayWeChat,
                 ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.Email"] = _ => _.DisplayEmail,
                 ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.其他"] = _ => _.DisplayOtherContact,
                 ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.单位名称"] = _ => _.DisplayOtherContact,
                 ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.地址"] = _ => _.DisplayOtherContact,
             };

        public static IDictionary<string, Func<DefaultUserAuthorize, bool>> DefaultUserAuthorizeDictionary { get; private set; }
            = new Dictionary<string, Func<DefaultUserAuthorize, bool>>(StringComparer.OrdinalIgnoreCase)
            {
                ["Desensitization.Dtos.AccountDto.OutIDNumber"] = _ => _.DisplayOutIDNumber,
                ["Desensitization.Dtos.AccountDto.ProvinceName"] = _ => _.DisplayProvinceName,
                ["Desensitization.Dtos.AccountDto.CityName"] = _ => _.DisplayCityName,
                ["Desensitization.Dtos.AccountDto.AccountNumber"] = _ => _.DisplayAccountNumber,
                ["Desensitization.Dtos.DebtorDto.IDNumber"] = _ => _.DisplayIDNumber,

                ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.Phone"] = _ => _.DisplayPhone,
                ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.身份证地址"] = _ => _.DisplayIDCardAddress,
                ["Desensitization.Dtos.AccountContactDto.DesensitizeContact.NotPhone"] = _ => _.DisplayOtherContact,
            };
    }
}