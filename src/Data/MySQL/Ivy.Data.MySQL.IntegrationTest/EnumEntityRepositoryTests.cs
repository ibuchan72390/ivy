using Ivy.Data.Core.Interfaces;
using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestValues;
using Ivy.TestUtilities;
using Ivy.TestUtilities.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Data.MySQL.IntegrationTest
{
    public class EnumEntityRepositoryTests : 
        MySqlIntegrationTestBase<IEnumEntityRepository<TestEnumEntity, TestEnum>>
    {
        #region SetUp & TearDown

        public EnumEntityRepositoryTests()
        {
            Sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        #endregion

        #region Tests

        #region GetByName

        [Fact]
        public void GetByName_Works_As_Expected_When_Exists()
        {
            TestEnum name = TestEnum.Test1;

            var entity = new TestEnumEntity { Name = name.ToString() }.SaveForTest();

            var result = Sut.GetByName(name);

            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public void GetByName_Works_As_Expected_When_Doesnt_Exist()
        {
            TestEnum name = TestEnum.Test1;

            var result = Sut.GetByName(name);

            Assert.Null(result);
        }

        [Fact]
        public void GetByName_Throws_Exception_When_Multiple_Mapped()
        {
            TestEnum name = TestEnum.Test1;

            new TestEnumEntity { Name = name.ToString() }.SaveForTest();
            new TestEnumEntity { Name = name.ToString() }.SaveForTest();

            var e = Assert.Throws<Exception>(() => Sut.GetByName(name));

            var expectedException = "Multiple objects found matching the provided Enumeration Value. " +
                $"Table: {typeof(TestEnumEntity).Name} / Search Value: {name.ToString()}";

            Assert.Equal(expectedException, e.Message);
        }

        #endregion

        #region GetByNameAsync

        [Fact]
        public async void GetByNameAsync_Works_As_Expected_When_Exists()
        {
            TestEnum name = TestEnum.Test1;

            var entity = new TestEnumEntity { Name = name.ToString() }.SaveForTest();

            var result = await Sut.GetByNameAsync(name);

            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public async void GetByNameAsync_Works_As_Expected_When_Doesnt_Exist()
        {
            TestEnum name = TestEnum.Test1;

            var result = await Sut.GetByNameAsync(name);

            Assert.Null(result);
        }

        [Fact]
        public async void GetByNameAsync_Throws_Exception_When_Multiple_Mapped()
        {
            TestEnum name = TestEnum.Test1;

            new TestEnumEntity { Name = name.ToString() }.SaveForTest();
            new TestEnumEntity { Name = name.ToString() }.SaveForTest();

            var e = await Assert.ThrowsAsync<Exception>(() => Sut.GetByNameAsync(name));

            var expectedException = "Multiple objects found matching the provided Enumeration Value. " +
                $"Table: {typeof(TestEnumEntity).Name} / Search Value: {name.ToString()}";

            Assert.Equal(expectedException, e.Message);
        }

        #endregion

        #region GetByNames

        [Fact]
        public void GetByNames_Returns_As_Expected_With_Results()
        {
            IList<TestEnum> enums = new List<TestEnum> { TestEnum.Test1, TestEnum.Test2 };

            var expected = enums.Select(x => new TestEnumEntity { Name = x.ToString() }.SaveForTest()).ToList();

            var results = Sut.GetByNames(enums);

            AssertExtensions.FullEntityListExclusion(expected, results);
        }

        [Fact]
        public void GetByNames_Returns_As_Expected_With_Results_And_TranConn()
        {
            var tranConnGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            IList<TestEnum> enums = new List<TestEnum> { TestEnum.Test1, TestEnum.Test2 };

            var expected = enums.Select(x => new TestEnumEntity { Name = x.ToString() }.SaveForTest()).ToList();

            var tc = tranConnGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var results = Sut.GetByNames(enums, tc);

            tc.Transaction.Commit();
            tc.Dispose();

            AssertExtensions.FullEntityListExclusion(expected, results);
        }

        [Fact]
        public void GetByNames_Returns_As_Expected_With_No_Results()
        {
            IList<TestEnum> enums = new List<TestEnum> { TestEnum.Test1, TestEnum.Test2 };

            var results = Sut.GetByNames(enums);

            Assert.Empty(results);
        }

        [Fact]
        public void GetByNames_Returns_As_Expected_For_Empty()
        {
            IList<TestEnum> enums = new List<TestEnum> {  };

            var results = Sut.GetByNames(enums);

            Assert.Empty(results);
        }

        [Fact]
        public void GetByNames_Returns_As_Expected_For_Null()
        {
            IList<TestEnum> enums = null;

            var results = Sut.GetByNames(enums);

            Assert.Empty(results);
        }

        #endregion

        #region GetByNamesAsync

        [Fact]
        public async void GetByNamesAsync_Returns_As_Expected_With_Results()
        {
            IList<TestEnum> enums = new List<TestEnum> { TestEnum.Test1, TestEnum.Test2 };

            var expected = enums.Select(x => new TestEnumEntity { Name = x.ToString() }.SaveForTest()).ToList();

            var results = await Sut.GetByNamesAsync(enums);

            AssertExtensions.FullEntityListExclusion(expected, results);
        }

        [Fact]
        public async void GetByNamesAsync_Returns_As_Expected_With_Results_And_TranConn()
        {
            var tranConnGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            IList<TestEnum> enums = new List<TestEnum> { TestEnum.Test1, TestEnum.Test2 };

            var expected = enums.Select(x => new TestEnumEntity { Name = x.ToString() }.SaveForTest()).ToList();

            var tc = tranConnGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var results = await Sut.GetByNamesAsync(enums, tc);

            tc.Transaction.Commit();
            tc.Dispose();

            AssertExtensions.FullEntityListExclusion(expected, results);
        }

        [Fact]
        public async void GetByNamesAsync_Returns_As_Expected_With_No_Results()
        {
            IList<TestEnum> enums = new List<TestEnum> { TestEnum.Test1, TestEnum.Test2 };

            var results = await Sut.GetByNamesAsync(enums);

            Assert.Empty(results);
        }

        [Fact]
        public async void GetByNamesAsync_Returns_As_Expected_For_Empty()
        {
            IList<TestEnum> enums = new List<TestEnum> { };

            var results = await Sut.GetByNamesAsync(enums);

            Assert.Empty(results);
        }

        [Fact]
        public async void GetByNamesAsync_Returns_As_Expected_For_Null()
        {
            IList<TestEnum> enums = null;

            var results = await Sut.GetByNamesAsync(enums);

            Assert.Empty(results);
        }

        #endregion

        #endregion

    }
}
