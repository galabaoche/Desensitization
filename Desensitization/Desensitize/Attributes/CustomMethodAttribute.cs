using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Desensitize.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomMethodAttribute : Attribute
    {
        private object _typeId;
        public override object TypeId
        {
            get { return _typeId ?? (_typeId = new object()); }
        }
        public CustomMethodAttribute(string methodFullName) : this(string.Empty, methodFullName) { }
        public CustomMethodAttribute(string ruleName, string methodFullName)
        {
            if (methodFullName == null)
            {
                throw new ArgumentNullException("methodFullName");
            }
            if (!methodFullName.Contains("."))
            {
                throw new InvalidOperationException("无效的方法名");
            }
            MethodFullName = methodFullName;
            RuleName = ruleName;
        }
        public string MethodFullName { get; set; }
        public string RuleName { get; set; }

        public void Desensitizate(ModelMetadata metadata)
        {
            if (metadata.Model == null)
            {
                throw new ArgumentNullException("metadata.Model ");
            }
            var typeName = MethodFullName.Substring(0, MethodFullName.LastIndexOf('.'));
            var methodName = MethodFullName.Substring(typeName.Length + 1);
            Type type = Type.GetType(typeName);
            if (type == null)
            {
                throw new InvalidOperationException($"无效的类型名{typeName}");
            }
            var methodInfo = type.GetMethod(methodName);
            if (methodInfo == null)
            {
                throw new InvalidOperationException($"{typeName}找不到有效的方法名{methodName}");
            }
            if (!methodInfo.IsStatic)
            {
                throw new InvalidOperationException($"仅支持静态方法");
            }
            if (methodInfo.ReturnType != typeof(string))
            {
                throw new InvalidOperationException($"方法返回值因为string类型");
            }
            methodInfo.Invoke(null, new object[] { metadata.Model }).ToString();
        }
    }
}