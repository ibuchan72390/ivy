/*
 * My main impetus for attempting this was to simplify the signature pattern for these methods;
 * however, the simplified signature separates the method from the interface.  While that can be
 * convenient, it prevents us from using a proper interface style in certain points
 */


using IBFramework.Data.Core.Interfaces.Domain;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace IBFramework.Utility.Extensions
{
    public static class EntityExtensions
    {
        #region Extension Methods

        public static int SafeGetIntRef<TSource, TProperty>(this TSource refEntity, Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<int>
        {
            var propertyName = GetPropertyInfo(processFn);

            var refName = propertyName + "Id";

            return (int)refEntity.References[refName];
        }

        public static string SafeGetStringRef<TSource, TProperty>(this TSource refEntity, Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<string>
        {
            var propertyName = GetPropertyInfo(processFn);

            var refName = propertyName + "Id";

            return (string)refEntity.References[refName];
        }

        #endregion

        #region Helper Methods

        private static string GetPropertyInfo<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            return propInfo.Name;
        }

        #endregion
    }
}
