using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Collections.Concurrent;
using System.Reflection;

namespace Oscar.Desensitization.Desensitize
{
    /// <summary>
    ///  将反射获取属性和设置属性封装为表达式树做编译缓存，防止大量反射的性能损伤
    /// </summary>
    public class PropertyExecutor
    {
        private static ConcurrentDictionary<string, Action<object, string>> propertySetdelegates = new ConcurrentDictionary<string, Action<object, string>>();
        private static ConcurrentDictionary<string, Func<object, object>> propertyGetdelegates = new ConcurrentDictionary<string, Func<object, object>>();
        public string ProperyName { get; private set; }
        public string TypeFullName { get; set; }
        private readonly string _setPropertyCacheKey;
        private readonly string _getPropertyCacheKey;
        public PropertyExecutor(string typeFullName, string propertyName)
        {
            this.TypeFullName = typeFullName;
            this.ProperyName = propertyName;
            _setPropertyCacheKey = $"{TypeFullName}_{propertyName}_set";
            _getPropertyCacheKey = $"{TypeFullName}_{propertyName}_get";
        }

        public void SetValue(object model, string value)
        {
            Action<object, string> propertyDelegate;
            if (propertySetdelegates.TryGetValue(_setPropertyCacheKey, out propertyDelegate))
            {
                propertyDelegate(model, value);
                return;
            }

            propertyDelegate = CreatePropertySetExecutor(model, value);
            propertySetdelegates[_setPropertyCacheKey] = propertyDelegate;
            propertyDelegate(model, value);
        }

        public object GetValue(object model)
        {
            Func<object, object> propertyDelegate;
            if (propertyGetdelegates.TryGetValue(_getPropertyCacheKey, out propertyDelegate))
            {
                return propertyDelegate(model);
            }

            propertyDelegate = CreatePropertyGetExecutor(model);
            propertyGetdelegates[_getPropertyCacheKey] = propertyDelegate;
            return propertyDelegate(model);
        }

        protected virtual Action<object, string> CreatePropertySetExecutor(object model, string value)
        {
            ParameterExpression target = Expression.Parameter(typeof(object), "model");
            ParameterExpression argument = Expression.Parameter(typeof(string), "argument");

            var propertyInfo = model.GetType().GetProperty(ProperyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"该类型没有名为{ProperyName}的属性");
            }

            var setMethod = propertyInfo.GetSetMethod(true);

            if (setMethod == null)
            {
                throw new InvalidOperationException($"该属性{ProperyName}是只读的");
            }
            var instancecast = Expression.Convert(target, model.GetType());
            var body = Expression.Call(instancecast, setMethod, argument);
            return Expression.Lambda<Action<object, string>>(body, target, argument).Compile();
        }

        public Func<object, object> CreatePropertyGetExecutor(object model)
        {
            ParameterExpression target = Expression.Parameter(typeof(object), "model");
            Type type = model.GetType();
            var propertyInfo = type.GetProperty(ProperyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"该类型没有名为{ProperyName}的属性");
            }
            var instancecast = Expression.Convert(target, model.GetType());
            var body = Expression.Property(instancecast, propertyInfo);
            UnaryExpression convertToObjectType = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<object, object>>(convertToObjectType, target).Compile();
        }
    }
}