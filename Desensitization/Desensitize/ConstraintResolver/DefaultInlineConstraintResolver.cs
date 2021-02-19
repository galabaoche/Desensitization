using Desensitization.Desensitize.Constraints;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Desensitization.Desensitize.ConstraintResolver
{
    /// <summary>
    /// 默认使用的约束检查
    /// </summary>
    public class DefaultInlineConstraintResolver : IInlineConstraintResolver
    {
        private ConcurrentDictionary<string, IConstraint> _constraintCache = new ConcurrentDictionary<string, IConstraint>();
        private readonly IDictionary<string, Type> _inlineConstraintMap = GetDefaultConstraintMap();

        public IDictionary<string, Type> ConstraintMap
        {
            get
            {
                return _inlineConstraintMap;
            }
        }

        private static IDictionary<string, Type> GetDefaultConstraintMap()
        {
            return new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
            {
                // Length constraints
                { "minlength", typeof(MinLengthConstraint) },
                { "maxlength", typeof(MaxLengthConstraint) },
                { "length", typeof(LengthConstraint) },
                
                //value constraints
                { "min", typeof(MinConstraint) },
                { "max", typeof(MaxConstraint) },
                { "range", typeof(RangeConstraint) },
                { "equal", typeof(EqualConstraint) },
                {"contains" ,typeof(ContainsConstraint)},

                {"invoke" ,typeof(MethodConstraint)},
                
                // Regex-based constraints
                { "regex", typeof(RegexConstraint) }
            };
        }

        public virtual IConstraint ResolveConstraint(string inlineConstraint)
        {
            if (inlineConstraint == null)
            {
                throw new ArgumentNullException("inlineConstraint");
            }
            IConstraint constraint;
            if (_constraintCache.TryGetValue(inlineConstraint,out constraint))
            {
                return constraint;
            }
           
            string constraintKey;
            string argumentString;
            int indexOfFirstOpenParens = inlineConstraint.IndexOf('(');
            if (indexOfFirstOpenParens >= 0 && inlineConstraint.EndsWith(")", StringComparison.Ordinal))
            {
                constraintKey = inlineConstraint.Substring(0, indexOfFirstOpenParens);
                argumentString = inlineConstraint.Substring(indexOfFirstOpenParens + 1, inlineConstraint.Length - indexOfFirstOpenParens - 2);
            }
            else
            {
                constraintKey = inlineConstraint;
                argumentString = null;
            }

            Type constraintType;
            if (!_inlineConstraintMap.TryGetValue(constraintKey, out constraintType))
            {
                return null;
            }

            if (!typeof(IConstraint).IsAssignableFrom(constraintType))
            {
                throw new InvalidOperationException("Invalid Constraint ");
            }

            constraint=(IConstraint)CreateConstraint(constraintType, argumentString);
            _constraintCache[inlineConstraint] = constraint;
            return constraint;

        }

        private static object CreateConstraint(Type constraintType, string argumentString)
        {
            if (argumentString == null)
            {
                return Activator.CreateInstance(constraintType);
            }

            ConstructorInfo activationConstructor = null;
            object[] parameters = null;
            ConstructorInfo[] constructors = constraintType.GetConstructors();

            if (constructors.Length == 1 && constructors[0].GetParameters().Length == 1)
            {
                activationConstructor = constructors[0];
                parameters = ConvertArguments(activationConstructor.GetParameters(), new string[] { argumentString });
            }
            else
            {
                string[] arguments = argumentString.Split(',').Select(argument => argument.Trim()).ToArray();

                ConstructorInfo[] matchingConstructors = constructors.Where(ci => ci.GetParameters().Length == arguments.Length).ToArray();
                int constructorMatches = matchingConstructors.Length;

                if (constructorMatches == 0)
                {
                    throw new InvalidOperationException("not matched constructor");
                }
                else if (constructorMatches == 1)
                {
                    activationConstructor = matchingConstructors[0];
                    parameters = ConvertArguments(activationConstructor.GetParameters(), arguments);
                }
                else
                {
                    throw new AmbiguousMatchException("mutiple matched constructor");
                }
            }
            return activationConstructor.Invoke(parameters);
        }

        private static object[] ConvertArguments(ParameterInfo[] parameterInfos, string[] arguments)
        {
            object[] parameters = new object[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                ParameterInfo parameter = parameterInfos[i];
                Type parameterType = parameter.ParameterType;
                parameters[i] = Convert.ChangeType(arguments[i], parameterType, CultureInfo.InvariantCulture);
            }
            return parameters;
        }

    }
}