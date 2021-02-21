using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Collections.Concurrent;

namespace Oscar.Desensitization.Desensitize
{
    /// <summary>
    /// 将需要掉用的方法封装为表达式树做编译缓存，防止大量反射的性能损伤
    /// </summary>
    public class ActionExecutor
    {
        private static ConcurrentDictionary<MethodInfo, object> delegates = new ConcurrentDictionary<MethodInfo, object>();
        public MethodInfo MethodInfo { get; private set; }

        public ActionExecutor(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
        }

        public object Execute(object instance, object[] arguments)
        {
            object actionOrFunc;
            if (delegates.TryGetValue(this.MethodInfo, out actionOrFunc))
            {
                return this.ExecuteCore(instance, arguments, actionOrFunc);
            }

            actionOrFunc = CreateExecutor(this.MethodInfo);
            delegates[this.MethodInfo] = actionOrFunc;
            return this.ExecuteCore(instance, arguments, actionOrFunc);
        }

        //执行指定的Action<object, object[]>或者Func<object, object[],object>对象
        protected virtual object ExecuteCore(object instance, object[] arguments, object executor)
        {
            Action<object, object[]> action = executor as Action<object, object[]>;
            if (null != action)
            {
                action(instance, arguments);
                return null;
            }

            Func<object, object[], object> func = executor as Func<object, object[], object>;
            if (null != func)
            {
                return func(instance, arguments);
            }

            return null;
        }

        //生成执行目标Action的表达式，并将其编译成Action<object, object[]>
        //或者Func<object, object[],object>对象
        protected virtual object CreateExecutor(MethodInfo methodInfo)
        {
            ParameterExpression target = Expression.Parameter(typeof(object), "target");
            ParameterExpression arguments = Expression.Parameter(typeof(object[]), "arguments");

            List<Expression> parameters = new List<Expression>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                ParameterInfo paramInfo = paramInfos[i];
                BinaryExpression getElementByIndex = Expression.ArrayIndex(arguments, Expression.Constant(i));
                UnaryExpression convertToParameterType = Expression.Convert(getElementByIndex, paramInfo.ParameterType);
                parameters.Add(convertToParameterType);
            }
            MethodCallExpression methodCall;
            if (methodInfo.IsStatic)
            {
                methodCall = Expression.Call( methodInfo, parameters);
            }
            else
            {
                UnaryExpression instanceCast = Expression.Convert(target, methodInfo.ReflectedType);
                 methodCall = Expression.Call(instanceCast, methodInfo, parameters);
            }

            if (methodInfo.ReflectedType == typeof(void))
            {
                return Expression.Lambda<Action<object, object[]>>(methodCall, target, arguments).Compile();
            }
            else
            {
                UnaryExpression convertToObjectType = Expression.Convert(methodCall, typeof(object));
                return Expression.Lambda<Func<object, object[], object>>(convertToObjectType, target, arguments).Compile();
            }
        }
    }
}