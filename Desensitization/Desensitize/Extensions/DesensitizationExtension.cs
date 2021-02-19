using Desensitization.Desensitize.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.Extensions
{
    /// <summary>
    /// 脱敏扩展类： 提供脱敏调用入口
    ///以及对脱敏特性封装为可直接调用的扩展方法，例如："12345".LeftDisplay(2);
    /// </summary>
    public static class DesensitizationExtension
    {
        public static void Desensitizate(this object instance)
        {
            if (instance==null)
            {
                throw new NullReferenceException("instance");
            }
            HttpContext.Current.SetUserAuthorize();
            var modelType = instance.GetType();
            bool isEnumerable = modelType.Match(typeof(IEnumerable<>));
            if (isEnumerable)
            {
                DesensitizationHandler.DesensitizationCollection(modelType.GetGenericArguments()[0], instance);
            }
            else
            {
                DesensitizationHandler.DesensitizationType(modelType,instance);
            }
            HttpContext.Current.ClearUserAuthorize();
        }
        public static bool Match(this Type type, Type typeToMatch)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeToMatch)
            {
                return true;
            }
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeToMatch)
                {
                    return true;
                }
            }
            return false;
        }

        #region Base DesensitizationAttributes Extention
        public static string LeftDisplay(this string source, int number)
        {
            return new LeftDisplayAttribute(number).Desensitizate(source);
        }
        public static string LeftHidden(this string source, int number)
        {
            return new LeftHiddenAttribute(number).Desensitizate(source);
        }
        public static string RightDisplayAttribute(this string source, int number)
        {
            return new RightDisplayAttribute(number).Desensitizate(source);
        }
        public static string RightHiddenAttribute(this string source, int number)
        {
            return new RightHiddenAttribute(number).Desensitizate(source);
        }
        public static string RangeMiddleDisplayAttribute(this string source, int min, int max)
        {
            return new RangeMiddleDisplayAttribute(min, max).Desensitizate(source);
        }
        public static string RangeMiddleHiddenAttribute(this string source, int min, int max)
        {
            return new RangeMiddleHiddenAttribute(min, max).Desensitizate(source);
        }
        public static string RangeSideDisplayAttribute(this string source, int left, int right)
        {
            return new RangeSideDisplayAttribute(left, right).Desensitizate(source);
        }
        public static string RangeSideHiddenAttribute(this string source, int left, int right)
        {
            return new RangeSideHiddenAttribute(left, right).Desensitizate(source);
        }
        public static string RegexInsertAttribute(this string source, string pattern, string replaceContent, RegexOptions regexOptions = RegexOptions.Compiled)
        {
            return new RegexInsertAttribute(pattern, replaceContent, regexOptions).Desensitizate(source);
        }
        public static string RegexReplaceAttribute(this string source, string pattern, string replaceContent, RegexOptions regexOptions = RegexOptions.Compiled)
        {
            return new RegexReplaceAttribute(pattern, replaceContent, regexOptions).Desensitizate(source);
        } 
        #endregion
    }
}