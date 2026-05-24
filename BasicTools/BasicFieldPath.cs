using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BasicTools
{
    public static class BasicFieldPath
    {
        public static LambdaExpression GetProperty<T>(string fullPath)
        {
            return GetNavigationPropertySelector(typeof(T), true, fullPath.Split('.'));
        }

        public static LambdaExpression GetPropertySelector<T>(string fullPath)
        {
            return GetNavigationPropertySelector(typeof(T), false, fullPath.Split('.'));
        }

        public static LambdaExpression GetProperty(Type type, string fullPath)
        {
            return GetNavigationPropertySelector(type, true, fullPath.Split('.'));
        }

        public static LambdaExpression GetPropertySelector(Type type, string fullPath)
        {
            return GetNavigationPropertySelector(type, false, fullPath.Split('.'));
        }

        public static void SetPropertyValue(object target, LambdaExpression memberLamda, object value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value, null);
                }
            }
        }

        public static LambdaExpression GetNavigationPropertySelector(Type type, bool last = false, params string[] properties)
        {
            return GetNavigationPropertySelector(type, properties, 0, last);
        }

        private static LambdaExpression GetNavigationPropertySelector(Type type, string[] properties, int depth, bool last = false)
        {
            var parameter = Expression.Parameter(type, depth == 0 ? "x" : "x" + depth);
            var body = GetNavigationPropertyExpression(parameter, properties, depth, last);
            return Expression.Lambda(body, parameter);
        }

        private static Expression GetNavigationPropertyExpression(Expression source, string[] properties, int depth, bool last = false)
        {
            if (depth >= properties.Length)
                return source;
            var property = Expression.Property(source, properties[depth]);
            if(last && properties.Length - 1 == depth)
            {
                return property;
            }
            if (typeof(IEnumerable).IsAssignableFrom(property.Type))
            {
                var elementType = property.Type.IsArray 
                    ? property.Type.GetElementType() 
                    : property.Type.GetGenericArguments()[0];
                var elementSelector = GetNavigationPropertySelector(elementType, properties, depth + 1, last);
                return Expression.Call(
                    typeof(Enumerable), "Select", new Type[] { elementType, elementSelector.Body.Type },
                    property, elementSelector);
            }
            else
            {
                return GetNavigationPropertyExpression(property, properties, depth + 1, last);
            }
        }
    }
}
