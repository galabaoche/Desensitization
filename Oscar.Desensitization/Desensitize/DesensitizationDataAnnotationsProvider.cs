using Oscar.Desensitization.Desensitize.Attributes;
using Oscar.Desensitization.Desensitize.ConstraintResolver;
using Oscar.Desensitization.Desensitize.Extensions;
using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oscar.Desensitization.Desensitize
{
    /// <summary>
    /// 自定义ModelMetadataProvider
    /// </summary>
    public class DesensitizationDataAnnotationsProvider : CachedDataAnnotationsModelMetadataProvider
    {
        protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
        {
            CachedDataAnnotationsModelMetadata modelMetadata = base.CreateMetadataPrototype(attributes, containerType, modelType, propertyName);

            var customMethodAttributes = attributes.OfType<CustomMethodAttribute>().ToList();
            if (customMethodAttributes != null && customMethodAttributes.Count() > 0)
            {
                modelMetadata.AdditionalValues.Add(DesensitizionKey.CustomMethodAttribute, customMethodAttributes);
            }

            //如果是类型不做处理
            if (modelMetadata.ContainerType == null)
            {
                return modelMetadata;
            }

            var propertyFullName = $"{modelMetadata.ContainerType.FullName}.{modelMetadata.PropertyName}";
            List<DesensitizationAttribute> matchedAttributes = new List<DesensitizationAttribute>();

            if (DesensitizationRuleDictionary.Rules.ContainsKey(propertyFullName))
            {
                var desensitizationAttribute = DesensitizationRuleDictionary.Rules[propertyFullName];
                matchedAttributes.AddRange(desensitizationAttribute);
            }

            var desensitizationAttributes = attributes.OfType<DesensitizationAttribute>().ToList();
            if (desensitizationAttributes != null && desensitizationAttributes.Count() > 0)
            {
                matchedAttributes.AddRange(desensitizationAttributes);
            }

            modelMetadata.AdditionalValues.Add(DesensitizionKey.DesensitizionAttribute, matchedAttributes);
            return modelMetadata;
        }

        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
        {
            CachedDataAnnotationsModelMetadata modelMetadata = base.CreateMetadataFromPrototype(prototype, modelAccessor);
            object objectAttributes;
            if (prototype.AdditionalValues.TryGetValue(DesensitizionKey.DesensitizionAttribute, out objectAttributes))
            {
                IList<DesensitizationAttribute> desensitizationAttributes = objectAttributes as IList<DesensitizationAttribute>;
                IList<DesensitizationAttribute> matchedAttributes = new List<DesensitizationAttribute>();

                if (desensitizationAttributes == null || desensitizationAttributes.Count() <= 0)
                {
                    return modelMetadata;
                }

                foreach (var desensitizationAttribute in desensitizationAttributes)
                {
                    var propertyFullName = $"{modelMetadata.ContainerType.FullName}.{modelMetadata.PropertyName}";
                    if (!string.IsNullOrEmpty(desensitizationAttribute.ShortDisplayName))
                    {
                        propertyFullName += $".{desensitizationAttribute.ShortDisplayName}";
                    }
                    var showForDisplay = desensitizationAttribute.RuleName.IsHasDisplayPermission(propertyFullName);
                    if (!showForDisplay)
                    {
                        matchedAttributes.Add(desensitizationAttribute);
                    }
                }

                modelMetadata.AdditionalValues.Add(DesensitizionKey.DesensitizionAttribute, matchedAttributes);
            }
            if (prototype.AdditionalValues.ContainsKey(DesensitizionKey.CustomMethodAttribute))
            {
                modelMetadata.AdditionalValues.Add(DesensitizionKey.CustomMethodAttribute,
                     prototype.AdditionalValues[DesensitizionKey.CustomMethodAttribute]);
            }
            return modelMetadata;
        }
    }
}