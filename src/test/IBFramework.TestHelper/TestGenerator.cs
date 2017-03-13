using IBFramework.Core.Data;
using IBFramework.Core.Data.Domain;
using IBFramework.Core.Utility;
using IBFramework.IoC;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestEntities.Base;
using System;

namespace IBFramework.TestHelper
{
    public static class TestExtensions
    {
        #region Variables & Constants

        //private static readonly IRandomizationHelper _rand =
        //    ServiceLocator.Instance.Resolve<IRandomizationHelper>();

        private static IRandomizationHelper _rand =>
            ServiceLocator.Instance.Resolve<IRandomizationHelper>();

        private static string connString = null;
        public static void Init(string connectionString)
        {
            connString = connectionString;
        }

        #endregion

        #region ChildEntity

        public static ChildEntity GenerateForTest(this ChildEntity entity, CoreEntity parent = null,
            string name = null, int? integer = null, decimal? deci = null)
        {
            RandomizeTestBase<ChildEntity>(entity);

            if (entity.CoreEntity == null)
                entity.CoreEntity = new CoreEntity().SaveForTest();

            return entity;
        }

        public static ChildEntity SaveForTest(this ChildEntity entity, CoreEntity parent = null,
            string name = null, int? integer = null, decimal? deci = null)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<ChildEntity, int>(created);
        }

        #endregion

        #region CoreEntity

        public static CoreEntity GenerateForTest(this CoreEntity entity)
        {
            RandomizeTestBase<CoreEntity>(entity);

            if (entity.ParentEntity == null)
                entity.ParentEntity = new ParentEntity().SaveForTest();

            return entity;
        }

        public static CoreEntity SaveForTest(this CoreEntity entity)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<CoreEntity, int>(created);
        }

        #endregion

        #region GuidIdEntity

        public static GuidIdEntity GenerateForTest(this GuidIdEntity entity)
        {
            RandomizeTestBase<GuidIdEntity>(entity);

            if (entity.CoreEntity == null)
                entity.CoreEntity = new CoreEntity().SaveForTest();

            return entity;
        }

        public static GuidIdEntity SaveForTest(this GuidIdEntity entity)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<GuidIdEntity, Guid>(created);
        }

        #endregion

        #region ParentEntity

        public static ParentEntity GenerateForTest(this ParentEntity entity)
        {
            RandomizeTestBase<ParentEntity>(entity);

            return entity;
        }

        public static ParentEntity SaveForTest(this ParentEntity entity)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<ParentEntity, int>(created);
        }

        #endregion

        #region StringIdEntity

        public static StringIdEntity GenerateForTest(this StringIdEntity entity)
        {
            RandomizeTestBase<StringIdEntity>(entity);

            if (entity.CoreEntity == null)
                entity.CoreEntity = new CoreEntity().SaveForTest();

            return entity;
        }

        public static StringIdEntity SaveForTest(this StringIdEntity entity)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<StringIdEntity, string>(created);
        }

        #endregion

        #region BlobEntity

        public static BlobEntity GenerateForTest(this BlobEntity entity)
        {
            RandomizeTestBase(entity);
            return entity;
        }

        public static BlobEntity SaveForTest(this BlobEntity entity)
        {
            var created = entity.GenerateForTest();

            SaveEntity<BlobEntity>(entity);

            return entity;
        }

        #endregion

        #region Helper Methods

        private static TEntity SaveEntity<TEntity, TKey>(TEntity entity)
            where TEntity : class, IEntityWithTypedId<TKey>
        {
            ValidateConnectionString();

            var repo = ServiceLocator.Instance.Resolve<IRepository<TEntity, TKey>>();
            repo.InitializeByConnectionString(connString);
            return repo.SaveOrUpdate(entity);
        }

        private static TEntity SaveEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            ValidateConnectionString();

            var repo = ServiceLocator.Instance.Resolve<IRepository<TEntity>>();
            repo.InitializeByConnectionString(connString);
            repo.Insert(entity);

            return entity;
        }

        private static void ValidateConnectionString()
        {
            if (connString == null)
            {
                throw new Exception("Connection string hasn't been initialized for the TestGenerator!");
            }
        }

        private static void RandomizeTestBase<T>(T entity)
            where T : BaseTestEntity
        {
            if (entity.Decimal == 0)
            {
                entity.Decimal = _rand.RandomDecimal();
            }

            if (string.IsNullOrEmpty(entity.Name))
            {
                entity.Name = _rand.RandomString();
            }

            if (entity.Integer == 0)
            {
                entity.Integer = _rand.RandomInt();
            }

            if (entity.Double == 0)
            {
                entity.Double = _rand.RandomDouble();
            }
        }

        #endregion
    }
}
