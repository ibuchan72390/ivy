using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IBFramework.Core.Data.Domain
{
    public interface IEntityWithReferences
    {
        Dictionary<string, object> References { get; set; }


        /*
         * There has GOT to be a better way to do this piece...
         * 
         * I'm not sure how to automatically type TSource to the current type of this
         */
        //int SafeGetIntReference<TSource, TProperty>(
        //    Expression<Func<TSource, TProperty>> propertyLambda)
        //    where TProperty : IEntityWithTypedId<int>;

        //string SafeGetStringReference<TSource, TProperty>(
        //    Expression<Func<TSource, TProperty>> propertyLambda)
        //    where TProperty : IEntityWithTypedId<string>;
    }
}
