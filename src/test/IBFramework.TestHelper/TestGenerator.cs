using IBFramework.Core.Data;
using IBFramework.Core.Data.Domain;
using IBFramework.Core.Utility;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestEntities.Base;
using System;

namespace IBFramework.TestHelper
{
    public static class TestExtensions
    {
        #region Variables & Constants

        private static readonly IRandomizationHelper _rand = 
            TestServiceLocator.StaticContainer.Resolve<IRandomizationHelper>();

        #endregion

        #region ChildEntity

        public static ChildEntity GenerateForTest(this ChildEntity entity, CoreEntity parent = null,
            string name = null, int? integer = null, decimal? deci = null)
        {
            RandomizeTestBase<ChildEntity, int>(entity);

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
            RandomizeTestBase<CoreEntity, int>(entity);

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
            RandomizeTestBase<GuidIdEntity, Guid>(entity);

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
            RandomizeTestBase<ParentEntity, int>(entity);

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
            RandomizeTestBase<StringIdEntity, string>(entity);

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

        #region Helper Methods

        private static TEntity SaveEntity<TEntity, TKey>(TEntity entity)
            where TEntity : IEntityWithTypedId<TKey>
        {
            var repo = TestServiceLocator.StaticContainer.Resolve<IRepository<TEntity, TKey>>();
            repo.InitializeByConnectionString(TestValues.TestDbConnectionString);
            return repo.SaveOrUpdate(entity);
        }

        private static void RandomizeTestBase<T, TKey>(T entity)
            where T : BaseTestEntity<TKey>, new()
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
        }

        #endregion
    }
}
