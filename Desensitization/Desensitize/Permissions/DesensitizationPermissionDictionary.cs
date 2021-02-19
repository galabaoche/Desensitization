using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Permissions
{
    /// <summary>
    /// 权限配置：采用完全限定名+属性名的方式
    /// </summary>
    public class DesensitizationPermissionDictionary
    {
        private const string AccountDtoName = "Desensitization.Dtos.AccountDto.";
        private const string DebtorDtoName = "Desensitization.Dtos.DebtorDto.";
        private const string ContactDtoName = "Desensitization.Dtos.AccountContactDto.";
        public static IDictionary<string, Func<PFUserAuthorize, bool>> PFUserAuthorizeDictionary { get; private set; }
             = new Dictionary<string, Func<PFUserAuthorize, bool>>(StringComparer.OrdinalIgnoreCase)
             {
                 [$"{AccountDtoName}OutIDNumber"] = _ => _.DisplayCard,
                 [$"{DebtorDtoName}IDNumber"] = _ => _.DisplayIdNumber,
                 [$"{DebtorDtoName}DebtorName"] = _ => _.DisplayDebtorName,
                 [$"{ContactDtoName}ContactName"] = _ => _.DisplayDebtorName,
                 [$"{ContactDtoName}DesensitizeContact.固话"] = _ => _.DisplayPhone,
                 [$"{ContactDtoName}DesensitizeContact.手机"] = _ => _.DisplayPhone,
                 [$"{ContactDtoName}DesensitizeContact.微信"] = _ => _.DisplayWeChat,
                 [$"{ContactDtoName}DesensitizeContact.Email"] = _ => _.DisplayEmail,
                 [$"{ContactDtoName}DesensitizeContact.其他"] = _ => _.DisplayOtherContact,
                 [$"{ContactDtoName}DesensitizeContact.单位名称"] = _ => _.DisplayOtherContact,
                 [$"{ContactDtoName}DesensitizeContact.地址"] = _ => _.DisplayOtherContact,
             };

        public static IDictionary<string, Func<DefaultUserAuthorize, bool>> DefaultUserAuthorizeDictionary { get; private set; }
            = new Dictionary<string, Func<DefaultUserAuthorize, bool>>(StringComparer.OrdinalIgnoreCase)
            {
                [$"{AccountDtoName}OutIDNumber"] = _ => _.DisplayOutIDNumber,
                [$"{AccountDtoName}ProvinceName"] = _ => _.DisplayProvinceName,
                [$"{AccountDtoName}CityName"] = _ => _.DisplayCityName,
                [$"{AccountDtoName}AccountNumber"] = _ => _.DisplayAccountNumber,
                [$"{DebtorDtoName}IDNumber"] = _ => _.DisplayIDNumber,

                [$"{ContactDtoName}DesensitizeContact.Phone"] = _ => _.DisplayPhone,
                [$"{ContactDtoName}DesensitizeContact.身份证地址"] = _ => _.DisplayIDCardAddress,
                [$"{ContactDtoName}DesensitizeContact.NotPhone"] = _ => _.DisplayOtherContact,
            };
    }
}