using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IBFramework.Core.Data.Domain
{
    public interface IEntityWithReferences
    {
        Dictionary<string, object> References { get; set; }

        //int SafeGetIntRef<TSource, TProperty>(Expression<Func<TSource, TProperty>> processFn)
        //    where TSource : IEntityWithReferences
        //    where TProperty : IEntityWithTypedId<int>;

        //string SafeGetStringRef<TSource, TProperty>(Expression<Func<TSource, TProperty>> processFn)
        //    where TSource : IEntityWithReferences
        //    where TProperty : IEntityWithTypedId<string>;
    }
}
