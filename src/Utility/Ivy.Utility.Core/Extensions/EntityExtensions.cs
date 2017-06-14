/*
 * My main impetus for attempting this was to simplify the signature pattern for these methods;
 * however, the simplified signature separates the method from the interface.  While that can be
 * convenient, it prevents us from using a proper interface style in certain points
 */


/*
 * This lives in Core as to prevent forcing consumers to reference the implementation project.
 * 
 * It's standard to reference the Core in other projects, just makes good sense to reference the interfaces,
 * then we let the IoC container determine the implementation at runtime.
 * 
 * As such, since we reference the interfaces anyway, it makes sense for these implementation to exist here.
 * Implementations should exist in Core for PURE FUNCTIONS ONLY
 */

using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ivy.Utility.Core.Extensions
{
    public static class EntityExtensions
    {
        #region Extension Methods

        public static int SafeGetIntRef<TSource, TProperty>(this TSource refEntity, Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<int>
        {
            return GetRefAndCast<TSource, TProperty, int>(refEntity, processFn);
        }

        public static string SafeGetStringRef<TSource, TProperty>(this TSource refEntity, Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<string>
        {
            return GetRefAndCast<TSource, TProperty, string>(refEntity, processFn);
        }

        #endregion

        #region Helper Methods

        private static TCast GetRefAndCast<TSource, TProperty, TCast>(this TSource refEntity, Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<TCast>
        {
            var propertyName = GetPropertyInfo(processFn);

            var refName = propertyName + "Id";

            return (TCast)refEntity.References[refName];
        }

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
