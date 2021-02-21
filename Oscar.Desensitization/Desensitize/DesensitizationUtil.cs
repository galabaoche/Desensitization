using Oscar.Desensitization.Desensitize.Attributes;
using Oscar.Desensitization.Desensitize.Permissions;
using System;
using System.Text;
using System.Web;

namespace Oscar.Desensitization.Desensitize
{
    public class DesensitizationUtil
    {
        public static bool IsNotPhoneNumber(string Phone)
        {
            return !IsPhoneNumber(Phone);
        }
        /// <summary>
        /// 是否电话
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string Phone)
        {
            try
            {
                int len = Phone.Length;
                Phone = System.Text.RegularExpressions.Regex.Replace(Phone, @"[^\d]*", "");
                //非汉字数
                if (len - Phone.Length < 4)
                {
                    Int64.Parse(Phone.Trim());
                    return Phone.Length < 14 && Phone.Length > 6;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 控制全国隐藏证件号（保留前后4位）
        /// </summary>
        /// <param name="outIdNumber"></param>
        /// <returns></returns>
        public static string FormateOutIDNumber(string outIdNumber)
        {
            //bool data1 = CacheManager.Get<bool>(CacheKeys.AllFormateOutIDNumber, "AllFormateOutIDNumber");
            //bool data2 = CacheManager.Get<bool>(CacheKeys.AllFormateIDNumber, "AllFormateIDNumber");
            if (!string.IsNullOrWhiteSpace(outIdNumber) && outIdNumber.Length > 8)
            {
                int count = outIdNumber.Length - 8;
                string left = outIdNumber.Substring(0, 4);
                string right = outIdNumber.Substring(outIdNumber.Length - 4, 4);
                outIdNumber = left;
                for (int i = 0; i < count; i++)
                {
                    outIdNumber += "*";
                }
                outIdNumber += right;
            }
            return outIdNumber;
        }

        /// <summary>
        /// 文本替换
        /// </summary>
        /// <param name="txt">数据文本</param>
        /// <param name="len">脱敏长度</param>
        /// <param name="newChar">脱敏字符</param>
        /// <returns></returns>
        public static string TxtReplace(string txt, int len, char newChar)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }
            int count = txt.Length > len ? len : txt.Length;
            var encodeTxt = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                encodeTxt.Append(newChar);
            }
            if (len >= txt.Length)
            {
                return encodeTxt.ToString();
            }
            int leftLength = (txt.Length - len) / 2;
            var result = new StringBuilder(txt.Substring(0, leftLength));
            result.Append(encodeTxt);
            result.Append(txt.Substring(leftLength + len));
            return result.ToString();
        }

        /// <summary>
        /// 隐藏手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string FormatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return "";
            }
            var contact = phoneNumber.Replace(" ", "");
            //
            var phone = System.Text.RegularExpressions.Regex.Replace(contact, @"[^\d]*", "");
            #region 固话

            //先过滤特殊字符（#*-括号等）
            //string contactTmp = contact.Replace("#", "").Replace("*", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "");
            //判断正则表达式
            var IsTel = System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\(?0(\d{2,3}\)?-?)?\d{7,8}$");
            if (IsTel && phone.Length >= 10 && phone.Length <= 12)
            {
                return TxtReplace(phoneNumber, 4, '*');
            }
            #endregion
            #region 手机
            //contactTmp = contact.Replace("#", "").Replace("*", "").Replace("-", "").TrimStart('0');
            var IsPhone = System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1([3456789][0-9]|4[579]|66|7[0135678]|9[89])[0-9]{8}$");
            if (IsPhone)
            {
                return TxtReplace(phoneNumber, 4, '*');
            }
            #endregion
            return phoneNumber;
        }

        public static string FormatePhoneNumberForPF(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return "";
            }

            #region 手机
            var contactTmp = phoneNumber.Replace(" ", "").Replace("#", "").Replace("*", "").Replace("-", "");

            var IsPhone = System.Text.RegularExpressions.Regex.IsMatch(contactTmp, @"^0{0,1}1([3456789][0-9]|4[579]|66|7[0135678]|9[89])[0-9]{8}$");
            if (IsPhone)
            {
                return new RightDisplayAttribute(4).Desensitizate(phoneNumber);
            }
            #endregion

            var phone = System.Text.RegularExpressions.Regex.Replace(contactTmp, @"[^\d]*", "");

            #region 固话
            var IsTel = System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\(?0(\d{2,3}\)?-?)?\d{7,8}$");
            var isHidden = System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{7,8}$");
            if (IsTel && phone.Length >= 10 && phone.Length <= 12 || isHidden)
            {
                return new RightDisplayAttribute(2).Desensitizate(phoneNumber);
            }
            #endregion
            return phoneNumber;
        }
    }
}