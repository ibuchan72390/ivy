using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Domain;
using Ivy.IoC;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestEntities.Base;
using Ivy.TestHelper.TestEntities.Flipped;
using Ivy.Utility.Core;
using System;

namespace Ivy.TestHelper
{
    public static class TestExtensions
    {
        #region Variables & Constants

        private static IRandomizationHelper _rand =>
            ServiceLocator.Instance.GetService<IRandomizationHelper>();

        private static string connString = null;
        public static void Init(string connectionString)
        {
            connString = connectionString;
        }

        #endregion

        #region ChildEntity

        public static ChildEntity GenerateForTest(this ChildEntity entity)
        {
            RandomizeTestBase<ChildEntity>(entity);

            if (entity.CoreEntity == null)
                entity.CoreEntity = new CoreEntity().GenerateForTest();

            return entity;
        }

        public static ChildEntity SaveForTest(this ChildEntity entity)
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
                entity.ParentEntity = new ParentEntity().GenerateForTest();

            if (entity.WeirdAlternateIntegerId == 0) entity.WeirdAlternateIntegerId = 1;
            if (entity.WeirdAlternateStringId == null) entity.WeirdAlternateStringId = "TEST";

            return entity;
        }

        public static CoreEntity SaveForTest(this CoreEntity entity, ITranConn tc = null)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<CoreEntity, int>(created, tc);
        }

        #endregion

        #region GuidIdEntity

        //public static GuidEntity GenerateForTest(this GuidEntity entity)
        //{
        //    RandomizeTestBase<GuidEntity>(entity);

        //    if (entity.CoreEntity == null)
        //        entity.CoreEntity = new CoreEntity().GenerateForTest();

        //    return entity;
        //}

        //public static GuidEntity SaveForTest(this GuidEntity entity, ITranConn tc = null)
        //{
        //    var created = entity.GenerateForTest();
        //    return SaveEntity<GuidEntity, Guid>(created);
        //}

        #endregion

        #region ParentEntity

        public static ParentEntity GenerateForTest(this ParentEntity entity)
        {
            RandomizeTestBase<ParentEntity>(entity);

            return entity;
        }

        public static ParentEntity SaveForTest(this ParentEntity entity, ITranConn tc = null)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<ParentEntity, int>(created, tc);
        }

        #endregion

        #region StringIdEntity

        public static StringEntity GenerateForTest(this StringEntity entity)
        {
            RandomizeTestBase<StringEntity>(entity);

            if (entity.CoreEntity == null)
                entity.CoreEntity = new CoreEntity().GenerateForTest();

            return entity;
        }

        public static StringEntity SaveForTest(this StringEntity entity, ITranConn tc = null)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<StringEntity, string>(created, tc);
        }

        #endregion

        #region FlippedStringEntity

        #region StringIdEntity

        public static FlippedStringEntity GenerateForTest(this FlippedStringEntity entity)
        {
            RandomizeTestBase<FlippedStringEntity>(entity);

            return entity;
        }

        public static FlippedStringEntity SaveForTest(this FlippedStringEntity entity, ITranConn tc = null)
        {
            var created = entity.GenerateForTest();
            return SaveEntity<FlippedStringEntity, string>(created, tc);
        }

        #endregion

        #endregion

        #region BlobEntity

        public static BlobEntity GenerateForTest(this BlobEntity entity)
        {
            RandomizeTestBase(entity);
            return entity;
        }

        public static BlobEntity SaveForTest(this BlobEntity entity, ITranConn tc = null)
        {
            var created = entity.GenerateForTest();

            SaveEntity<BlobEntity>(entity, tc);

            return entity;
        }

        #endregion

        #region FlippedBlobEntity

        public static FlippedBlobEntity GenerateForTest(this FlippedBlobEntity entity)
        {
            RandomizeTestBase(entity);
            return entity;
        }

        public static FlippedBlobEntity SaveForTest(this FlippedBlobEntity entity, ITranConn tc = null)
        {
            var created = entity.GenerateForTest();

            SaveEntity<FlippedBlobEntity>(entity, tc);

            return entity;
        }

        #endregion

        #region TestEnumEntity

        public static TestEnumEntity GenerateForTest(this TestEnumEntity entity)
        {
            if (entity.Name == null) entity.Name = _rand.RandomString();
            if (entity.FriendlyName == null) entity.FriendlyName = entity.Name;

            return entity;
        }

        public static TestEnumEntity SaveForTest(this TestEnumEntity entity, ITranConn tc = null)
        {
            entity = entity.GenerateForTest();

            return SaveEntity<TestEnumEntity, int>(entity, tc);
        }

        #endregion

        #region Helper Methods

        private static TEntity SaveEntity<TEntity, TKey>(TEntity entity, ITranConn tc = null)
            where TEntity : class, IEntityWithTypedId<TKey>
        {
            ValidateConnectionString();

            var repo = ServiceLocator.Instance.GetService<IEntityRepository<TEntity, TKey>>();
            repo.InitializeByConnectionString(connString);
            return repo.SaveOrUpdate(entity, tc);
        }

        private static TEntity SaveEntity<TEntity>(TEntity entity, ITranConn tc = null)
            where TEntity : class
        {
            ValidateConnectionString();

            var repo = ServiceLocator.Instance.GetService<IBlobRepository<TEntity>>();
            repo.InitializeByConnectionString(connString);
            repo.Insert(entity, tc);

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
            where T : IBaseTestEntity
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

            entity.Boolean = _rand.RandomBool();
        }

        #endregion
    }
}
