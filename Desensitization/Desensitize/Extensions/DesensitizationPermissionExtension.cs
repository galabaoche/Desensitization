using Desensitization.Desensitize.Permissions;
using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Extensions
{
    /// <summary>
    /// 脱敏权限扩展
    /// </summary>
    public static class DesensitizationPermissionExtension
    {
        public static void SetUserAuthorize(this HttpContext context)
        {
            SetPFUserAuthorize(context);
            SetDefaultUserAuthorize(context);
        }
        public static PFUserAuthorize SetPFUserAuthorize(this HttpContext context)
        {
            //HttpContextWrapper contextBase = new HttpContextWrapper(context);
            //int userId = contextBase.GetCurrentUserId();
            //PFUserAuthorize pfUserAuthorize = new PFUserAuthorize();

            //bool IsCard = UserViewRangeUtil.GetUserAuthorizeByPermission(userId, UserPermissionType.Card);
            //bool IsIdNumber = UserViewRangeUtil.GetUserAuthorizeByPermission(userId, UserPermissionType.IDNumber);
            //bool IsDebtorName = UserViewRangeUtil.GetUserAuthorizeByPermission(userId, UserPermissionType.DebtorName);
            //bool IsAddress = UserViewRangeUtil.GetUserAuthorizeByPermission(userId, UserPermissionType.Address);
            //bool IsEmail = UserViewRangeUtil.GetUserAuthorizeByPermission(userId, UserPermissionType.Email);
            //bool IsPhone = UserViewRangeUtil.GetUserAuthorizeByPermission(userId, UserPermissionType.Phone);
            //bool IsWeChat = UserViewRangeUtil.GetUserAuthorizeByPermission(userId, UserPermissionType.WeChat);

            bool IsCard = false;
            bool IsIdNumber = false;
            bool IsDebtorName = false;
            bool IsAddress = false;
            bool IsEmail = false;
            bool IsPhone = false;
            bool IsWeChat = false;

            PFUserAuthorize pfUserAuthorize = new PFUserAuthorize()
            {
                DisplayOtherContact = IsAddress,
                DisplayCard = IsCard,
                DisplayDebtorName = IsDebtorName,
                DisplayEmail = IsEmail,
                DisplayIdNumber = IsIdNumber,
                DisplayPhone = IsPhone,
                DisplayWeChat = IsWeChat,
            };
            context.Items[DesensitizionKey.PFUserAuthorize] = pfUserAuthorize;
            return pfUserAuthorize;
        }
        public static DefaultUserAuthorize SetDefaultUserAuthorize(this HttpContext context)
        {
            // HttpContextWrapper contextBase = new HttpContextWrapper(context);
            //int userId = contextBase.GetCurrentUserId();
            //var user = contextBase.GetCurrentUser();

            //bool IsForbiddenPhoneNumber = CacheManager.Get<bool>(CacheKeys.UnitForbiddenPhoneNumber, user.UnitId.ToString());
            //bool IsBankWorkerGoToUnit = GlobalData.IsBankWorkerGoToUnit(user.UnitId);
            //bool UserData = CacheManager.Get<bool>(CacheKeys.UserDesensitizationKey, userId.ToString());
            //bool IpData = CacheManager.Get<bool>(CacheKeys.IpDesensitizationKey, userId.ToString());
            //bool IsHiddenAccountNo = CacheManager.Get<bool>(CacheKeys.UnitFormateAccountNo, user.UnitId.ToString());
            //bool Data1 = CacheManager.Get<bool>(CacheKeys.UnitFormateOutIDNumber, user.UnitId.ToString());
            //bool Data2 = CacheManager.Get<bool>(CacheKeys.UnitFormateIDNumber, user.UnitId.ToString());

            bool isForbiddenPhoneNumber = true;
            bool isBankWorkerGoToUnit = true;
            bool userdata = true;
            bool ipdata = true;
            bool isHiddenAccountNo = true;
            bool data1 = true;
            bool data2 = true;


            DefaultUserAuthorize defaultAuthorize = new DefaultUserAuthorize()
            {
                DisplayOutIDNumber = !(userdata || ipdata || data1),
                DisplayIDNumber = !(userdata || ipdata || data2),
                DisplayProvinceName = !isBankWorkerGoToUnit,
                DisplayCityName = !isBankWorkerGoToUnit,
                DisplayAccountNumber = !(isHiddenAccountNo || userdata || ipdata),
                DisplayPhone = !(isForbiddenPhoneNumber || userdata || ipdata),
                DisplayOtherContact = !(isBankWorkerGoToUnit || userdata || ipdata),
                DisplayIDCardAddress = !isBankWorkerGoToUnit,
            };
            context.Items[DesensitizionKey.DefaultUserAuthorize] = defaultAuthorize;
            return defaultAuthorize;
        }

        public static void ClearUserAuthorize(this HttpContext context)
        {
            if (context.Items.Contains(DesensitizionKey.PFUserAuthorize))
            {
                context.Items.Remove(DesensitizionKey.PFUserAuthorize);
            }
            if (context.Items.Contains(DesensitizionKey.DefaultUserAuthorize))
            {
                context.Items.Remove(DesensitizionKey.DefaultUserAuthorize);
            }
        }

        /// <summary>
        /// 供外部调用的权限检查方法
        /// 按照配置权限进行脱敏，无配置的话： 自定义规则默认脱敏，默认规则默认不脱敏
        /// </summary>
        /// <param name="ruleName"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static bool IsHasDisplayPermission(this string ruleName, string displayName)
        {
            switch (ruleName)
            {
                case DesensitizionKey.PF:
                    var pFUserAuthorize = HttpContext.Current.Items[DesensitizionKey.PFUserAuthorize] as PFUserAuthorize;
                    if (pFUserAuthorize == null)
                    {
                        return false;
                    }
                    Func<PFUserAuthorize, bool> pFUserAuthorizeAccessor;
                    if (!DesensitizationPermissionDictionary.PFUserAuthorizeDictionary.TryGetValue(displayName, out pFUserAuthorizeAccessor))
                    {
                        return false;
                    }
                    return pFUserAuthorizeAccessor(pFUserAuthorize);
                case "":
                    var defaultUserAuthorize = HttpContext.Current.Items[DesensitizionKey.DefaultUserAuthorize] as DefaultUserAuthorize;
                    if (defaultUserAuthorize == null)
                    {
                        return true;
                    }
                    Func<DefaultUserAuthorize, bool> defaultUserAuthorizeAccessor;
                    if (!DesensitizationPermissionDictionary.DefaultUserAuthorizeDictionary.TryGetValue(displayName, out defaultUserAuthorizeAccessor))
                    {
                        return true;
                    }
                    return defaultUserAuthorizeAccessor(defaultUserAuthorize);
                default:
                    return false;
            }
        }
        public static PFUserAuthorize GetPFUserAuthorize(this HttpContext context)
        {
            return context.Items[DesensitizionKey.PFUserAuthorize] as PFUserAuthorize;
        }
        public static DefaultUserAuthorize GetDefaultUserAuthorize(this HttpContext context)
        {
            return context.Items[DesensitizionKey.DefaultUserAuthorize] as DefaultUserAuthorize;
        }
    }
}