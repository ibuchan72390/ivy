using IBFramework.Core.Data.Domain;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace IBFramework.Core.Domain
{
    public class BaseEntityWithReferences : IEntityWithReferences
    {
        #region Variables & Constants

        public Dictionary<string, object> References { get; set; }

        private const string ReferenceTail = "Id";

        #endregion

        #region Extension Methods

        public int SafeGetIntRef<TSource, TProperty>(Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<int>
        {
            var propertyName = GetPropertyInfo(processFn);

            var refName = propertyName + "Id";

            return (int)References[refName];
        }

        public string SafeGetStringRef<TSource, TProperty>(Expression<Func<TSource, TProperty>> processFn)
            where TSource : IEntityWithReferences
            where TProperty : IEntityWithTypedId<string>
        {
            var propertyName = GetPropertyInfo(processFn);

            var refName = propertyName + "Id";

            return (string)References[refName];
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
