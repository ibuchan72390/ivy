using Ivy.Data.Core.Interfaces;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.Utility.Core.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Data.Common.Test.Repository
{
    public class EnumEntityRepositoryTests :
        RepositoryTestBase<IEnumEntityRepository<TestEnumEntity, TestEnum>>
    {
        #region Tests

        #region GetByName

        [Fact]
        public void GetByName_Executes_As_Expected_With_TranConn()
        {
            TestEnum enumVal = TestEnum.Test1;

            var entity = new TestEnumEntity { Name = enumVal.ToString() };

            _mockEntitySqlGen.Setup(x => x.GenerateGetQuery(null, "WHERE `THIS`.`Name` = @enumVal", null, null, null, null)).Returns(sql);

            _mockTranHelper.
                Setup(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object)).
                Returns(new List<TestEnumEntity> { entity });

            var result = Sut.GetByName(enumVal, tc.Object);

            Assert.Same(entity, result);

            _mockEntitySqlGen.Verify(x => x.GenerateGetQuery(null, "WHERE `THIS`.`Name` = @enumVal", null, null, null, null), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetByNameAsync

        [Fact]
        public async void GetByNameAsync_Executes_As_Expected_With_TranConn()
        {
            TestEnum enumVal = TestEnum.Test1;

            var entity = new TestEnumEntity { Name = enumVal.ToString() };

            _mockEntitySqlGen.Setup(x => x.GenerateGetQuery(null, "WHERE `THIS`.`Name` = @enumVal", null, null, null, null)).Returns(sql);

            _mockTranHelper.
                Setup(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object)).
                ReturnsAsync(new List<TestEnumEntity> { entity });

            var result = await Sut.GetByNameAsync(enumVal, tc.Object);

            Assert.Same(entity, result);

            _mockEntitySqlGen.Verify(x => x.GenerateGetQuery(null, "WHERE `THIS`.`Name` = @enumVal", null, null, null, null), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetByNames

        [Fact]
        public void GetByNames_Executes_As_Expected_With_TranConn()
        {
            var enumVals = EnumUtility.GetValues<TestEnum>();

            string idInList = enumVals.Select(x => $"'{x.ToString()}'").
                Aggregate((total, current) => $"{total}, {current}");

            string sqlWhere = $"WHERE `THIS`.`Name` IN ({idInList})";

            var entities = enumVals.Select(x => new TestEnumEntity { Name = x.ToString() }).ToList();

            _mockEntitySqlGen.Setup(x => x.GenerateGetQuery(null, sqlWhere, null, null, null, null)).Returns(sql);

            _mockTranHelper.
                Setup(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object)).
                Returns(entities);

            var result = Sut.GetByNames(enumVals, tc.Object);

            Assert.Same(entities, result);

            _mockEntitySqlGen.Verify(x => x.GenerateGetQuery(null, sqlWhere, null, null, null, null), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetByNamesAsync

        [Fact]
        public async void GetByNamesAsync_Executes_As_Expected_With_TranConn()
        {
            var enumVals = EnumUtility.GetValues<TestEnum>();

            string idInList = enumVals.Select(x => $"'{x.ToString()}'").
                Aggregate((total, current) => $"{total}, {current}");

            string sqlWhere = $"WHERE `THIS`.`Name` IN ({idInList})";

            var entities = enumVals.Select(x => new TestEnumEntity { Name = x.ToString() }).ToList();

            _mockEntitySqlGen.Setup(x => x.GenerateGetQuery(null, sqlWhere, null, null, null, null)).Returns(sql);

            _mockTranHelper.
                Setup(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object)).
                ReturnsAsync(entities);

            var result = await Sut.GetByNamesAsync(enumVals, tc.Object);

            Assert.Same(entities, result);

            _mockEntitySqlGen.Verify(x => x.GenerateGetQuery(null, sqlWhere, null, null, null, null), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #endregion
    }
}
