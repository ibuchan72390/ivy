using Ivy.TestHelper.TestEntities;
using Ivy.Utility.Core.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class EntityExtensionsTests
    {
        #region SafeGetIntRef

        [Fact]
        public void SafeGetIntRef_Properly_Returns_Id_For_Integer_Reference()
        {
            const int idVal = 123;

            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", idVal }
            };

            var result = entity.SafeGetIntRef<CoreEntity, ParentEntity>(x => x.ParentEntity);

            Assert.Equal(idVal, result);
        }

        [Fact]
        public void SafeGetIntRef_Properly_Returns_Id_For_Nullable_Integer_Reference()
        {
            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", DBNull.Value }
            };

            var result = entity.SafeGetIntRef<CoreEntity, ParentEntity>(x => x.ParentEntity);

            Assert.Equal(0, result);
        }

        #endregion

        #region SafeGetNullableIntRef

        [Fact]
        public void SafeGetNullableIntRef_Properly_Returns_Id_For_Integer_Reference()
        {
            const int idVal = 123;

            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", idVal }
            };

            var result = entity.SafeGetNullableIntRef<CoreEntity, ParentEntity>(x => x.ParentEntity);

            Assert.Equal(idVal, result);
        }

        [Fact]
        public void SafeGetNullableIntRef_Properly_Returns_Id_For_Nullable_Integer_Reference()
        {
            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", DBNull.Value }
            };

            var result = entity.SafeGetNullableIntRef<CoreEntity, ParentEntity>(x => x.ParentEntity);

            Assert.Equal(null, result);
        }

        #endregion

        #region SafeGetStringRef

        [Fact]
        public void SafeGetStringRef_Properly_Returns_Id_For_String_Reference()
        {
            const string idVal = "TEST";

            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "StringIdEntityId", idVal }
            };

            var result = entity.SafeGetStringRef<CoreEntity, StringEntity>(x => x.StringIdEntity);

            Assert.Equal(idVal, result);
        }

        #endregion

        #region RebuildChildEntitiesFromReferences

        [Fact]
        public void RebuildChildEntitiesFromReferences_Can_Populate_Int_Id_Entity()
        {
            const int idVal = 123;

            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", idVal }
            };

            entity.RebuildChildEntitiesFromRefs();

            Assert.NotNull(entity.ParentEntity);
            Assert.Equal(idVal, entity.ParentEntity.Id);
        }

        [Fact]
        public void RebuildChildEntitiesFromReferences_Can_Populate_String_Id_Entity()
        {
            const string idVal = "TEST";

            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "StringIdEntityId", idVal }
            };

            entity.RebuildChildEntitiesFromRefs();

            Assert.NotNull(entity.StringIdEntity);
            Assert.Equal(entity.StringIdEntity.Id, idVal);
        }

        [Fact]
        public void RebuildChildEntitiesFromReferences_Skips_Null_Reference()
        {
            var entity = new CoreEntity();

            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", null }
            };

            entity.RebuildChildEntitiesFromRefs();

            Assert.Null(entity.ParentEntity);
        }

        [Fact]
        public void RebuildChildEntitiesFromReferences_Skips_DBNull_Reference()
        {
            var entity = new CoreEntity();

            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", DBNull.Value }
            };

            entity.RebuildChildEntitiesFromRefs();

            Assert.Null(entity.ParentEntity);
        }

        #endregion
    }
}
