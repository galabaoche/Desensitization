using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Desensitization.Desensitize.Constraints
{
    /// <summary>
    /// 自定义方法检测是否通过约束
    /// </summary>
    public class MethodConstraint : IConstraint
    {
        public MethodConstraint(string methodFullName)
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
        }

        public string MethodFullName { get; private set; }
        public bool Match(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
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
            if (methodInfo.ReturnType != typeof(bool))
            {
                throw new InvalidOperationException($"方法返回值因为bool类型");
            }
            ActionExecutor executor = new ActionExecutor(methodInfo);
            return Convert.ToBoolean(executor.Execute(null, new object[] { value }));
            //return Convert.ToBoolean(methodInfo.Invoke(null, new object[] { value }));
        }
    }
}