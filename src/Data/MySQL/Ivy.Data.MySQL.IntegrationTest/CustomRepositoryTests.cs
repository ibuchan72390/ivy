using Ivy.IoC;
using System.Collections.Generic;
using Ivy.Utility.IoC;
using Xunit;
using System.Linq;
using System;
using Ivy.Utility.Core.Extensions;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.TestHelper.TestEntities;
using Ivy.Data.Common;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Ivy.TestHelper.TestValues;
using Ivy.Data.Common.IoC;
using Ivy.IoC.IoC;
using Ivy.MySQL.IoC;
using Ivy.TestUtilities;
using Ivy.Data.Core.Interfaces.Pagination;
using Ivy.Data.Common.Pagination;
using Ivy.TestUtilities.Utilities;

namespace Ivy.Data.MySQL.IntegrationTest
{
    public class CustomRepositoryTests : MySqlIntegrationTestBase, IDisposable
    {
        #region Variables & Constants

        private ICustomParentEntityRepository _sut;

        private interface ICustomParentEntityRepository : IEntityRepository<ParentEntity>
        {
            #region GetBasicTypeList

            string GetNameById(int id, ITranConn tc = null);

            IEnumerable<int> GetAllIds(ITranConn tc = null);

            #endregion

            #region InternalSelect

            IEnumerable<ParentEntity> GetAllByIdDesc(ITranConn tc = null);

            IEnumerable<ParentEntity> GetByCoreEntityId(int coreEntityId, ITranConn tc = null);

            IEnumerable<ParentEntity> GetByName(string name, ITranConn tc = null);

            IEnumerable<ParentEntity> GetTop5(ITranConn tc = null);

            #endregion

            #region PaginatedSelect

            IPaginationResponse<ParentEntity> SearchByName(IPaginationRequest request, ITranConn tc = null);

            #endregion

            #region InternalUpdate

            void UpdateNameById(int id, string newName, ITranConn tc = null);

            void UpdateAllNames(string newName, ITranConn tc = null);

            #endregion

            #region InternalDelete

            void DeleteByName(string name, ITranConn tc = null);

            #endregion
        }

        private class CustomParentEntityRepository : 
            EntityRepository<ParentEntity>, 
            ICustomParentEntityRepository
        {
            public CustomParentEntityRepository(
                IDatabaseKeyManager databaseKeyManager, 
                ITransactionHelper tranHelper, 
                ISqlGenerator<ParentEntity, int> sqlGenerator) 
                : base(databaseKeyManager, tranHelper, sqlGenerator)
            {
            }

            #region GetBasicTypeList

            public string GetNameById(int id, ITranConn tc = null)
            {
                const string sql = "SELECT `Name` FROM `parententity` WHERE `Id` = @id";

                var parms = new Dictionary<string, object>
                {
                    { "@id", id }
                };

                return GetBasicTypeList<string>(sql, parms, tc).FirstOrDefault();
            }

            public IEnumerable<int> GetAllIds(ITranConn tc = null)
            {
                const string sql = "SELECT `Id` FROM `parententity`";

                return GetBasicTypeList<int>(sql, null, tc);
            }

            #endregion

            #region InteralSelect

            public IEnumerable<ParentEntity> GetAllByIdDesc(ITranConn tc = null)
            {
                const string sqlOrder = "ORDER BY `Id` DESC";

                return InternalSelect(orderByClause: sqlOrder, tc: tc);
            }

            public IEnumerable<ParentEntity> GetByCoreEntityId(int coreEntityId, ITranConn tc = null)
            {
                const string sqlJoin = "JOIN CoreEntity CORE ON (THIS.Id = CORE.ParentEntityId)";
                const string sqlWhere = "WHERE CORE.Id = @coreEntityId";

                var parms = new Dictionary<string, object>();
                parms.Add("@coreEntityId", coreEntityId);

                return InternalSelect(null, sqlJoin, sqlWhere, null, null, null, parms, tc);
            }

            public IEnumerable<ParentEntity> GetByName(string name, ITranConn tc = null)
            {
                const string sqlWhere = "WHERE THIS.Name = @entityName";

                var parms = new Dictionary<string, object>();
                parms.Add("@entityName", name);

                return InternalSelect(null, null, sqlWhere, null, null, null, parms, tc);
            }

            public IEnumerable<ParentEntity> GetTop5(ITranConn tc = null)
            {
                return InternalSelect(limit: 5);
            }

            #endregion

            #region PaginatedSelect

            public IPaginationResponse<ParentEntity> SearchByName(IPaginationRequest request, ITranConn tc = null)
            {
                const string sqlWhere = "WHERE THIS.Name LIKE CONCAT('%', @search, '%')";

                var parms = new Dictionary<string, object> { { "@search", request.Search } };

                return InternalSelectPaginated(null, null, sqlWhere, null, request, parms, tc);
            }

            #endregion

            #region InternalUpdate

            public void UpdateAllNames(string newName, ITranConn tc = null)
            {
                const string setClause = "SET `Name` = @newName";

                var parms = new Dictionary<string, object> { {"@newName", newName } };

                InternalUpdate(setClause, null, parms, tc);
            }

            public void UpdateNameById(int id, string newName, ITranConn tc = null)
            {
                const string setClause = "SET `Name` = @newName";
                const string sqlWhere = "WHERE `Id` = @id";

                var parms = new Dictionary<string, object> { { "@newName", newName }, { "@id", id } };

                InternalUpdate(setClause, sqlWhere, parms, tc);
            }

            #endregion

            #region InternalDelete

            public void DeleteByName(string name, ITranConn tc = null)
            {
                const string sqlWhere = "WHERE `Name` = @name";

                var parms = new Dictionary<string, object>
                {
                    { "@name", name }
                };

                InternalDelete(sqlWhere, parms, tc);
            }

            #endregion
        }

        #endregion

        #region Constructor

        public CustomRepositoryTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            containerGen.InstallIvyIoC();
            containerGen.InstallIvyUtility();

            containerGen.InstallIvyCommonData();
            containerGen.InstallIvyMySql();

            containerGen.RegisterSingleton<ICustomParentEntityRepository, CustomParentEntityRepository>();

            var _container = containerGen.GenerateContainer();

            _sut = _container.GetService<ICustomParentEntityRepository>();

            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        public void Dispose()
        {
            TestCleaner.CleanDatabase();
        }

        #endregion

        #region Tests

        #region GetBasicTypeList

        [Fact]
        public void GetNameById_Returns_As_Expected()
        {
            var targetEntity = new ParentEntity().SaveForTest();

            var result = _sut.GetNameById(targetEntity.Id);

            Assert.Equal(targetEntity.Name, result);
        }

        [Fact]
        public void GetNameById_Returns_As_Expected_When_No_Result()
        {
            var result = _sut.GetNameById(-1);
            Assert.Null(result);
        }

        [Fact]
        public void GetAllIds_Returns_As_Expected()
        {
            const int toMake = 5;

            var entities = Enumerable.Range(0, toMake).
                Select(x => new ParentEntity().SaveForTest()).
                ToList();

            var entityIds = entities.Select(x => x.Id);

            var results = _sut.GetAllIds();

            AssertExtensions.FullBasicListExclusion(entityIds, results);
        }

        [Fact]
        public void GetAllIds_Returns_As_Expected_When_No_Result()
        {
            var results = _sut.GetAllIds();

            Assert.Empty(results);
        }

        #endregion

        #region InternalSelect

        [Fact]
        public void CustomOrderBy_Works_As_Expected()
        {
            var targetCount = 5;

            var entities = Enumerable.Range(0, targetCount).
                Select(x => new ParentEntity().SaveForTest()).
                ToList();

            var results = _sut.GetAllByIdDesc().ToList();

            int? currentId = null;
            foreach (var result in results)
            {
                if (currentId.HasValue)
                {
                    Assert.True(result.Id < currentId.Value);
                }

                currentId = result.Id;
            }
        }

        [Fact]
        public void CustomWhereClause_Query_Works_As_Expected()
        {
            var targetEntity = new ParentEntity().SaveForTest();

            var results = _sut.GetByName(targetEntity.Name);

            Assert.Single(results);
        }

        [Fact]
        public void SelectPrefix_Query_Works_As_Expected()
        {
            var allParentEntities = Enumerable.Range(0, 10).Select(x => new ParentEntity().SaveForTest()).ToList();

            var expectedParentEntities = allParentEntities.Take(5);

            var results = _sut.GetTop5();

            Assert.Equal(5, results.Count());

            var resultIds = results.Select(x => x.Id);
            var expectedIds = expectedParentEntities.Select(x => x.Id);

            Assert.Empty(resultIds.Except(expectedIds));
            Assert.Empty(expectedIds.Except(resultIds));
        }

        [Fact]
        public void SqlJoin_Query_Works_As_Expected()
        {
            var entity = new ParentEntity().SaveForTest();

            var coreEntity = new CoreEntity { ParentEntity = entity }.SaveForTest();

            Enumerable.Range(0, 3).Select(x => new CoreEntity { ParentEntity = entity }.SaveForTest()).ToList();

            var results = _sut.GetByCoreEntityId(coreEntity.Id);

            Assert.Single(results);

            var result = results.First();

            Assert.Equal(entity.Id, result.Id);
        }

        #endregion

        #region InternalPaginatedSelect

        [Fact]
        public void FindByName_Works_As_Expected_With_Pagination()
        {
            const string nameLike = "TEST";
            const int viableEntities = 4;

            var expectedEntities = Enumerable.Range(0, viableEntities).
                Select(x => new ParentEntity { Name = $"{nameLike}{x}" }.SaveForTest()).
                ToList();

            var alternateEntities = Enumerable.Range(0, 4).
                Select(x => new ParentEntity().SaveForTest()).
                ToList();

            var req = new PaginationRequest();
            req.Search = nameLike;
            req.PageCount = 10;
            req.PageNumber = 1;

            var results = _sut.SearchByName(req);

            // This is a bad assumption...
            //Assert.Equal(viableEntities * 2, results.TotalCount);
            // We want to know the total with the filter applied
            // This way, we properly understand client-side pagination.
            // Otherwise, total page count will never change

            Assert.Equal(viableEntities, results.TotalCount);
            Assert.Equal(viableEntities, results.Data.Count());
            AssertExtensions.FullEntityListExclusion(expectedEntities, results.Data);
        }

        [Fact]
        public void FindByName_Works_As_Expected_With_Limited_Pagination()
        {
            const string nameLike = "TEST";
            const int viableEntities = 4;
            const int pageCount = 2;

            var expectedEntities = Enumerable.Range(0, viableEntities).
                Select(x => new ParentEntity { Name = $"{nameLike}{x}" }.SaveForTest()).
                ToList();

            var expected = expectedEntities.Take(2);

            var alternateEntities = Enumerable.Range(0, 4).
                Select(x => new ParentEntity().SaveForTest()).
                ToList();

            var req = new PaginationRequest();
            req.Search = nameLike;
            req.PageCount = pageCount;
            req.PageNumber = 1;

            var results = _sut.SearchByName(req);

            // See above as to why this is a bad assumption
            //Assert.Equal(viableEntities * 2, results.TotalCount);

            Assert.Equal(viableEntities, results.TotalCount);
            Assert.Equal(pageCount, results.Data.Count());
            AssertExtensions.FullEntityListExclusion(expected, results.Data);
        }

        #endregion

        #region InternalUpdate

        [Fact]
        public void Custom_Update_Works_As_Expected()
        {
            const string newName = "My Sample Name";

            var entities = Enumerable.Range(0, 3).Select(x => new ParentEntity().SaveForTest()).ToList();

            _sut.UpdateAllNames(newName);

            var results = _sut.GetAll();

            results.Each(x => Assert.Equal(newName, x.Name));
        }

        [Fact]
        public void Custom_Update_With_Where_Clause_Works_As_Expected()
        {
            const string newName = "My Sample Name";

            var entities = Enumerable.Range(0, 3).Select(x => new ParentEntity().SaveForTest()).ToList();

            var updateEntity = entities.First();

            _sut.UpdateNameById(updateEntity.Id, newName);

            var results = _sut.GetAll();

            foreach (var result in results)
            {
                if (result.Id == updateEntity.Id)
                {
                    Assert.Equal(newName, result.Name);
                }
                else
                {
                    Assert.NotEqual(newName, result.Name);
                }
            }
        }

        #endregion

        #region InternalDelete

        [Fact]
        public void Delete_With_Custom_Where_Works_As_Expected()
        {
            var entity = new ParentEntity().SaveForTest();

            Assert.NotNull(_sut.GetById(entity.Id));

            _sut.DeleteByName(entity.Name);

            Assert.Null(_sut.GetById(entity.Id));
        }

        #endregion

        #endregion

    }
}
