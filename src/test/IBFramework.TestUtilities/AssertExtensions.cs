using IBFramework.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IBFramework.TestUtilities
{
    public static class AssertExtensions
    {
        public static void FullBasicListExclusion<T>(IEnumerable<T> items, IEnumerable<T> incoming)
        {
            Assert.Empty(items.Except(incoming));
            Assert.Empty(incoming.Except(items));
        }

        public static void FullEntityListExclusion<TEntity>(IEnumerable<TEntity> items, IEnumerable<TEntity> incoming)
            where TEntity : class, IEntity
        {
            var itemIds = items.Select(x => x.Id);
            var incomingIds = incoming.Select(x => x.Id);

            FullBasicListExclusion(itemIds, incomingIds);
        }

        public static void FullEntityWithTypedIdListExclusion<TEntity, TKey>(IEnumerable<TEntity> items, IEnumerable<TEntity> incoming)
            where TEntity : class, IEntityWithTypedId<TKey>
        {
            var itemIds = items.Select(x => x.Id);
            var incomingIds = incoming.Select(x => x.Id);

            FullBasicListExclusion<TKey>(itemIds, incomingIds);
        }

        public static void EnumEqual<TEnum>(IEnumEntity<TEnum> entity1, IEnumEntity<TEnum> entity2)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            Assert.Equal(entity1.Id, entity2.Id);
            Assert.Equal(entity1.Name, entity2.Name);
            Assert.Equal(entity1.FriendlyName, entity2.FriendlyName);
            Assert.Equal(entity1.SortOrder, entity2.SortOrder);
        }
    }
}
