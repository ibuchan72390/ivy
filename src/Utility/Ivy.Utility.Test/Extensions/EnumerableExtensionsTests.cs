using Ivy.Utility.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class EnumerableExtensionsTests
    {
        #region Variables & Constants

        private class EachTestEntity
        {
            public int Integer { get; set; }
        }

        #endregion

        #region Tests

        [Fact]
        public void Each_Appropriately_Affects_Every_Item_In_Collection()
        {
            const int toMake = 5;

            var entities = Enumerable.Range(0, toMake).Select(x => new EachTestEntity { Integer = 1 });

            IList<int> testCol = new List<int>();

            entities.Each(x => testCol.Add(x.Integer));

            Assert.Equal(toMake, testCol.Count);
        }

        [Fact]
        public void IsNullOrEmpty_Returns_True_If_Null()
        {
            IList<int> list = null;
            Assert.True(list.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullorEmpty_Returns_True_If_Empty()
        {
            IList<int> list = new List<int>();
            Assert.True(list.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_Returns_False_If_Has_Data()
        {
            IList<int> list = new List<int> { 0 };
            Assert.False(list.IsNullOrEmpty());
        }

        #endregion
    }
}
