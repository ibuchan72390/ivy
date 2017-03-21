using IBFramework.Core.Data;
using IBFramework.Core.IoC;
using IBFramework.Data.Common;
using IBFramework.IoC;
using IBFramework.TestHelper.TestEntities;
using System.Collections.Generic;
using IBFramework.Core.Data.SQL;
using IBFramework.Utility.IoC;
using IBFramework.Caching.IoC;
using IBFramework.IoC.Installers;
using IBFramework.Data.Common.IoC;
using IBFramework.MySQL.IoC;
using Xunit;
using IBFramework.TestHelper;
using System.Linq;
using IBFramework.TestHelper.TestValues;
using System;

namespace IBFramework.Data.MySQL.IntegrationTest
{
    public class CustomRepositoryTests : MySqlIntegrationTestBase, IDisposable
    {
        #region Variables & Constants

        private ICustomParentEntityRepository _sut;

        private interface ICustomParentEntityRepository : IEntityRepository<ParentEntity>
        {
            IEnumerable<ParentEntity> GetByCoreEntityId(int coreEntityId, ITranConn tc = null);

            IEnumerable<ParentEntity> GetByName(string name, ITranConn tc = null);

            IEnumerable<ParentEntity> GetTop5(ITranConn tc = null);
        }

        private class CustomParentEntityRepository : EntityRepository<ParentEntity>, ICustomParentEntityRepository
        {
            public CustomParentEntityRepository(
                IDatabaseKeyManager databaseKeyManager, 
                ITransactionHelper tranHelper, 
                ISqlGenerator<ParentEntity, int> sqlGenerator) 
                : base(databaseKeyManager, tranHelper, sqlGenerator)
            {
            }

            public IEnumerable<ParentEntity> GetByCoreEntityId(int coreEntityId, ITranConn tc = null)
            {
                const string sqlJoin = "JOIN CoreEntity CORE ON (THIS.Id = CORE.ParentEntityId)";
                const string sqlWhere = "WHERE CORE.Id = @coreEntityId";

                var parms = new Dictionary<string, object>();
                parms.Add("@coreEntityId", coreEntityId);

                return FindBy(sqlJoin, sqlWhere, null, null, parms, tc);
            }

            public IEnumerable<ParentEntity> GetByName(string name, ITranConn tc = null)
            {
                const string sqlWhere = "WHERE THIS.Name = @entityName";

                var parms = new Dictionary<string, object>();
                parms.Add("@entityName", name);

                return FindBy(null, sqlWhere, null, null, parms, tc);
            }

            public IEnumerable<ParentEntity> GetTop5(ITranConn tc = null)
            {
                return FindBy(limit: 5);
            }
        }

        #endregion

        #region Constructor

        public CustomRepositoryTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            containerGen.InstallIoC();
            containerGen.InstallCaching();
            containerGen.InstallUtility();

            containerGen.InstallCommonData();
            containerGen.InstallMySql();

            containerGen.RegisterSingleton<ICustomParentEntityRepository, CustomParentEntityRepository>();

            var _container = containerGen.GenerateContainer();

            _sut = _container.Resolve<ICustomParentEntityRepository>();

            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        public void Dispose()
        {
            TestCleaner.CleanDatabase();
        }

        #endregion

        #region Tests

        [Fact]
        public void CustomWhereClause_Query_Works_As_Expected()
        {
            var targetEntity = new ParentEntity().SaveForTest();

            var results = _sut.GetByName(targetEntity.Name);

            Assert.Equal(1, results.Count());
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

            var results = _sut.GetByCoreEntityId(coreEntity.Id);

            Assert.Equal(1, results.Count());

            var result = results.First();

            Assert.Equal(entity.Id, result.Id);
        }

        #endregion

    }
}
