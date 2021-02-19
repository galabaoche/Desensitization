using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.Attributes
{
    /// <summary>
    /// 标记特性，比如AccountDto具有DebtorDto，
    /// 要对DebtorDto里的IDNumber和DebtorName脱敏，需要使用此特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DesensitizationContainerAttribute : Attribute, IMetadataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.Watermark = DesensitizionKey.DesensitizionContainerAttribute;
        }
    }
}