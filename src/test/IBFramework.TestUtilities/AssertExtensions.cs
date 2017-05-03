using IBFramework.Core.Data.Domain;
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
    }
}
