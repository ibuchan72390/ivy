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
        #region Variables & Constants

        private const string idStr = "Id";

        #endregion

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

        public static void RebuildChildEntitiesFromRefs<T>(this T entity)
            where T : IEntityWithReferences
        {
            var props = typeof(T).GetRuntimeProperties();

            foreach (var refVal in entity.References)
            {
                // If we remove Id from the reference, we should have the property name
                // We should just need to target the reference, set it to a new object and set its Id
                var attr = refVal.Key.Substring(0, refVal.Key.LastIndexOf(idStr));

                PropertyInfo targetAttr = null;
                foreach (var prop in props)
                {
                    if (prop.Name == attr)
                    {
                        targetAttr = prop;
                        break;
                    }
                }

                if (refVal.Value == null) continue;

                var targetType = refVal.Value.GetType();

                // It appears that this is an issue for null values I guess
                if (targetType == typeof(System.DBNull)) continue;

                if (targetType == typeof(string))
                {
                    AssignPropertyFromIdAndType<T, string>(entity, refVal.Key, targetAttr);
                }
                else if (targetType == typeof(int))
                {
                    AssignPropertyFromIdAndType<T, int>(entity, refVal.Key, targetAttr);
                }
                else
                {
                    throw new Exception($"Unable to map Id of unexpected type! Received Type: {targetType.Name}");
                }
            }
        }

        #endregion

        #region Helper Methods

        private static void AssignPropertyFromIdAndType<TEntity, TCast>(TEntity entity, string keyId, PropertyInfo targetAttr)
            where TEntity : IEntityWithReferences
        {
            // Now we need to identify the type of the item we're creating
            IEntityWithTypedId<TCast> createdEntity = (IEntityWithTypedId<TCast>)Activator.CreateInstance(targetAttr.PropertyType);

            var stringId = GetCastReference<TCast>(entity, keyId);

            createdEntity.Id = stringId;

            targetAttr.SetValue(entity, createdEntity);
        }

        private static TCast GetRefAndCast<TSource, TProperty, TCast>(this TSource refEntity, Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<TCast>
        {
            var propertyName = GetPropertyInfo(processFn);

            var refName = propertyName + idStr;

            //return (TCast)refEntity.References[refName];
            return GetCastReference<TCast>(refEntity, refName);
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

        private static TReturn GetCastReference<TReturn>(IEntityWithReferences refEntity, string refKey)
        {
            return (TReturn)refEntity.References[refKey];
        }

        #endregion
    }
}
