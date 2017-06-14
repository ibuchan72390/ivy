using Ivy.Utility.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void Each_Appropriately_Affects_Every_Item_In_Collection()
        {
            const int toMake = 5;

            var entities = Enumerable.Range(0, toMake).Select(x => new EachTestEntity { Integer = 1 });

            IList<int> testCol = new List<int>();

            entities.Each(x => testCol.Add(x.Integer));

            Assert.Equal(toMake, testCol.Count);
        }

        private class EachTestEntity
        {
            public int Integer { get; set; }
        }
    }
}
