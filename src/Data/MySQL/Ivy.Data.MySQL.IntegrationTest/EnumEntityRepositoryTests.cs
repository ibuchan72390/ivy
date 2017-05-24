using Ivy.Data.Core.Interfaces;
using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestValues;
using System;
using Xunit;

namespace Ivy.Data.MySQL.IntegrationTest
{
    public class EnumEntityRepositoryTests : MySqlIntegrationTestBase
    {
        #region Variables & Constants

        private readonly IEnumEntityRepository<TestEnumEntity, TestEnum> _sut;

        #endregion

        #region SetUp & TearDown

        public EnumEntityRepositoryTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IEnumEntityRepository<TestEnumEntity, TestEnum>>();
            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        #endregion

        #region Tests

        #region GetByName

        [Fact]
        public void GetByName_Works_As_Expected_When_Exists()
        {
            TestEnum name = TestEnum.Test1;

            var entity = new TestEnumEntity { Name = name.ToString() }.SaveForTest();

            var result = _sut.GetByName(name);

            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public void GetByName_Works_As_Expected_When_Doesnt_Exist()
        {
            TestEnum name = TestEnum.Test1;

            var result = _sut.GetByName(name);

            Assert.Null(result);
        }

        [Fact]
        public void GetByName_Throws_Exception_When_Multiple_Mapped()
        {
            TestEnum name = TestEnum.Test1;

            new TestEnumEntity { Name = name.ToString() }.SaveForTest();
            new TestEnumEntity { Name = name.ToString() }.SaveForTest();

            var e = Assert.Throws<Exception>(() => _sut.GetByName(name));

            var expectedException = "Multiple objects found matching the provided Enumeration Value. " +
                $"Table: {typeof(TestEnumEntity).Name} / Search Value: {name.ToString()}";

            Assert.Equal(expectedException, e.Message);
        }

        #endregion

        #endregion

    }
}
