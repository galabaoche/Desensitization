using Desensitization.Desensitize.Attributes;
using Desensitization.Desensitize.ConstraintResolver;
using Desensitization.Desensitize.Extensions;
using Desensitization.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize
{
    public class DesensitizationHandler
    {
        public static void DesensitizationCollection(Type elementType, object source)
        {
            MethodInfo DesensitizationInvoke = typeof(DesensitizationHandler).GetMethod("DesensitizationInvoke", BindingFlags.Static | BindingFlags.NonPublic);
            DesensitizationInvoke.MakeGenericMethod(elementType).Invoke(null, new object[] { source });
        }

        /// <summary>
        /// 此方法直接不能调用，只能反射调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private static void DesensitizationInvoke<T>(IEnumerable<T> source)
        {
            var instanceList = source.ToList();
            for (int i = 0; i < instanceList.Count(); i++)
            {
                DesensitizationType(typeof(T), instanceList[i]);
            }
        }
        public static void DesensitizationType(Type type, object instance)
        {
            var CurrentModelMetadataProvider = ModelMetadataProviders.Current;
            ModelMetadataProviders.Current = new DesensitizationDataAnnotationsProvider();
            try
            {
                var metadata = ModelMetadataProviders.Current.GetMetadataForType(() => instance, type);
                object attribute;
                if (metadata.AdditionalValues.TryGetValue(DesensitizionKey.CustomMethodAttribute, out attribute))
                {
                    var customMethodAttributes = attribute as IList<CustomMethodAttribute>;
                    if (customMethodAttributes != null && customMethodAttributes.Count() > 0)
                    {
                        foreach (var customMethodAttribute in customMethodAttributes)
                        {
                            if (customMethodAttribute.RuleName == metadata.Description ||
                                (string.IsNullOrEmpty(customMethodAttribute.RuleName) && string.IsNullOrEmpty(metadata.Description)))
                            {
                                customMethodAttribute.Desensitizate(metadata);
                                return;
                            }
                        }
                    }
                }

                foreach (var propertyMetadata in metadata.Properties)
                {
                    if (propertyMetadata.AdditionalValues != null && propertyMetadata.AdditionalValues.Count() > 0
                            || propertyMetadata.Watermark == DesensitizionKey.DesensitizionContainerAttribute)
                    {
                        DesensitizationProperty(propertyMetadata, metadata.Description);
                    }
                }
                instance = metadata.Model;
            }
            finally
            {
                ModelMetadataProviders.Current = CurrentModelMetadataProvider;
            }

        }
        private static void DesensitizationProperty(ModelMetadata metadata, string ruleName)
        {
            if (metadata.Watermark == DesensitizionKey.DesensitizionContainerAttribute)
            {
                foreach (var propertyMetadata in metadata.Properties)
                {
                    DesensitizationProperty(propertyMetadata, ruleName);
                }
            }

            object attribute;
            if (metadata.AdditionalValues.TryGetValue(DesensitizionKey.DesensitizionAttribute, out attribute))
            {
                var desensitizationAttributes = attribute as IList<DesensitizationAttribute>;
                if (desensitizationAttributes == null)
                {
                    return;
                }

                var defaultRuleNameAttributes = desensitizationAttributes
                    .Where(d => string.IsNullOrEmpty(d.RuleName)).OrderBy(d => d.Order).ToList();
                var customRuleNameAttributes = desensitizationAttributes
                    .Where(d => !string.IsNullOrEmpty(d.RuleName)).OrderBy(d => d.Order).ToList();

                bool matched = DesensitizeByAttibutes(metadata, customRuleNameAttributes, _ => _ == ruleName);
                if (!matched)
                {
                    DesensitizeByAttibutes(metadata, defaultRuleNameAttributes, _ => string.IsNullOrEmpty(_));
                }
            }
        }
        private static bool DesensitizeByAttibutes(ModelMetadata metadata, List<DesensitizationAttribute> ruleNameAttributes, Func<string, bool> predicate)
        {
            bool matched = false;
            foreach (var desensitizationAttribute in ruleNameAttributes)
            {
                if (desensitizationAttribute != null && predicate(desensitizationAttribute.RuleName))
                {
                    if (!string.IsNullOrEmpty(desensitizationAttribute.InlineConstraint)
                        && !ConstraintCheck.Match(desensitizationAttribute.InlineConstraint, metadata))
                    {
                        continue;
                    }

                    if (desensitizationAttribute.Condition != null
                        && !desensitizationAttribute.Condition(metadata.Model.ToString()))
                    {
                        continue;
                    }
                    matched = true;
                    metadata.Model = desensitizationAttribute.Desensitizate(metadata.Model.ToString());
                    //防止循环中频繁反射损伤性能，使用表达式树做了编译缓存
                    PropertyExecutor executor = new PropertyExecutor(metadata.ContainerType.FullName, metadata.PropertyName);
                    executor.SetValue(metadata.Container, metadata.Model.ToString());
                    //metadata.ContainerType.GetProperty(metadata.PropertyName).SetValue(metadata.Container, metadata.Model);
                    break;
                }
            }
            return matched;
        }
    }
}