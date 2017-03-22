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

        #region Public Methods

        //public int SafeGetIntReference<TSource, TProperty>(
        //    Expression<Func<TSource, TProperty>> propertyLambda)
        //    where TProperty : IEntityWithTypedId<int>
        //{
        //    var propertyName = GetPropertyInfo(propertyLambda);

        //    var key = $"{propertyName}{ReferenceTail}";

        //    if (References.ContainsKey(key))
        //    {
        //        // Cast with failure
        //        return (int)References[key];
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}

        //public string SafeGetStringReference<TSource, TProperty>(
        //    Expression<Func<TSource, TProperty>> propertyLambda)
        //    where TProperty : IEntityWithTypedId<string>
        //{
        //    var propertyName = GetPropertyInfo(propertyLambda);

        //    var key = $"{propertyName}{ReferenceTail}";

        //    if (References.ContainsKey(key))
        //    {
        //        // Cast with failure
        //        return (string)References[key];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #endregion

        #region Helper Methods

        //private T SafeGetReference<T>(string propertyName)
        //{
        //    var key = $"{propertyName}{ReferenceTail}";

        //    if (References.ContainsKey(key))
        //    {
        //        return (T)References[key];
        //    }
        //    else
        //    {
        //        return default(T);
        //    }
        //}



        #endregion
    }
}
