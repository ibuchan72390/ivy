using Ivy.TestHelper.TestEntities;
using Ivy.TestUtilities;
using Ivy.Utility.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class IntersectAndExcludeExtensionsTests
    {
        #region IntersectAndExcludeValues

        [Fact]
        public void IntersectAndExcludeValues_ProperlyIdentifies_ExistingOnly()
        {
            const int toMake = 3;

            var incoming = new List<int>();
            var existing = Enumerable.Range(0, toMake);

            var ier = existing.IntersectAndExcludeValues(incoming);

            AssertExtensions.FullBasicListExclusion(ier.ExistingOnly, existing);
            AssertExtensions.FullBasicListExclusion(ier.ExistingOverlapping, new List<int>());
            AssertExtensions.FullBasicListExclusion(ier.IncomingOverlapping, new List<int>());
            AssertExtensions.FullBasicListExclusion(ier.IncomingOnly, new List<int>());
        }

        [Fact]
        public void IntersectAndExcludeValues_ProperlyIdentifies_Overlapping()
        {
            const int toMake = 3;

            // This should create a single overlapping
            var existing = Enumerable.Range(toMake - 2, toMake);
            var incoming = Enumerable.Range(0, toMake);

            var overlapping = existing.Intersect(incoming);

            var incomingOnly = incoming.Except(existing);
            var existingOnly = existing.Except(incoming);

            var ier = existing.IntersectAndExcludeValues(incoming);

            AssertExtensions.FullBasicListExclusion(ier.ExistingOnly, existingOnly);
            AssertExtensions.FullBasicListExclusion(ier.ExistingOverlapping, overlapping);
            AssertExtensions.FullBasicListExclusion(ier.IncomingOverlapping, overlapping);
            AssertExtensions.FullBasicListExclusion(ier.IncomingOnly, incomingOnly);
        }

        [Fact]
        public void IntersectAndExcludeValues_ProperlyIdentifies_IncomingOnly()
        {
            const int toMake = 3;

            var existing = new List<int>();
            var incoming = Enumerable.Range(0, toMake);

            var ier = existing.IntersectAndExcludeValues(incoming);

            AssertExtensions.FullBasicListExclusion(ier.ExistingOnly, new List<int>());
            AssertExtensions.FullBasicListExclusion(ier.ExistingOverlapping, new List<int>());
            AssertExtensions.FullBasicListExclusion(ier.IncomingOverlapping, new List<int>());
            AssertExtensions.FullBasicListExclusion(ier.IncomingOnly, incoming);
        }

        #endregion

        #region IntersectAndExcludeEntities

        [Fact]
        public void IntersectAndExcludeEntities_ProperlyIdentifies_ExistingOnly()
        {
            const int toMake = 3;

            var incoming = new List<ParentEntity>();
            var existing = Enumerable.Range(0, toMake).Select(x => new ParentEntity { Id = x });

            var ier = existing.IntersectAndExcludeEntities(incoming);

            AssertExtensions.FullEntityListExclusion(ier.ExistingOnly, existing);
            AssertExtensions.FullEntityListExclusion(ier.ExistingOverlapping, new List<ParentEntity>());
            AssertExtensions.FullEntityListExclusion(ier.IncomingOverlapping, new List<ParentEntity>());
            AssertExtensions.FullEntityListExclusion(ier.IncomingOnly, new List<ParentEntity>());
        }

        [Fact]
        public void IntersectAndExcludeEntities_ProperlyIdentifies_Overlapping()
        {
            const int toMake = 3;

            // This should create a single overlapping
            var existing = Enumerable.Range(toMake - 2, toMake).Select(x => new ParentEntity { Id = x });
            var incoming = Enumerable.Range(0, toMake).Select(x => new ParentEntity { Id = x });

            var overlapping = existing.Intersect(incoming);

            var incomingIds = incoming.Select(x => x.Id);
            var existingIds = existing.Select(x => x.Id);

            var incomingOnly = incoming.Where(x => !existingIds.Contains(x.Id));
            var incomingOverlapping = incoming.Where(x => existingIds.Contains(x.Id));

            var existingOnly = existing.Where(x => !incomingIds.Contains(x.Id));
            var existingOVerlapping = existing.Where(x => incomingIds.Contains(x.Id));

            var ier = existing.IntersectAndExcludeEntities(incoming);

            AssertExtensions.FullEntityListExclusion(ier.ExistingOnly, existingOnly);
            AssertExtensions.FullEntityListExclusion(ier.ExistingOverlapping, existingOVerlapping);
            AssertExtensions.FullEntityListExclusion(ier.IncomingOverlapping, incomingOverlapping);
            AssertExtensions.FullEntityListExclusion(ier.IncomingOnly, incomingOnly);
        }

        [Fact]
        public void IntersectAndExcludeEntities_ProperlyIdentifies_IncomingOnly()
        {
            const int toMake = 3;

            var existing = new List<ParentEntity>();
            var incoming = Enumerable.Range(0, toMake).Select(x => new ParentEntity { Id = x });

            var ier = existing.IntersectAndExcludeEntities(incoming);

            AssertExtensions.FullEntityListExclusion(ier.ExistingOnly, new List<ParentEntity>());
            AssertExtensions.FullEntityListExclusion(ier.ExistingOverlapping, new List<ParentEntity>());
            AssertExtensions.FullEntityListExclusion(ier.IncomingOverlapping, new List<ParentEntity>());
            AssertExtensions.FullEntityListExclusion(ier.IncomingOnly, incoming);
        }

        #endregion
    }
}
