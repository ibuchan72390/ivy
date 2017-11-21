using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestEntities.Flipped;
using Ivy.TestUtilities;
using Ivy.TestUtilities.Utilities;
using Ivy.Utility.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class EntityExtensionsTests : TestBase
    {
        #region Tests

        #region ToEntity

        [Fact]
        public void ToEntity_Works_With_Int_Entities()
        {
            var id = 123;

            var result = id.ToEntity<ParentEntity>();

            Assert.Equal(id, result.Id);
        }

        #endregion

        #region ToEntityWithTypedId

        [Fact]
        public void ToEntityWithTypedId_Works_With_String_Entities()
        {
            var id = "TEST";

            var result = id.ToEntityWithTypedId<StringEntity, string>();

            Assert.Equal(id, result.Id);
        }

        #endregion

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

        #region MapEntityChildren

        [Fact]
        public void MapEntityChildren_Assigns_Entities_As_Expected()
        {
            // Need to demonstrate that we can bring in an IEnumerable source and children
            // Children will have references pointing to source objects
            // Source objects should get child mappings
            // Dictionary is probably your best bet

            const int entityCount = 4;
            const int perCount = 2;

            var entities = Enumerable.Range(0, entityCount).
                Select(x => new CoreEntity { Id = x }.GenerateForTest()).
                ToList();

            Dictionary<CoreEntity, IEnumerable<ChildEntity>> dict = 
                new Dictionary<CoreEntity, IEnumerable<ChildEntity>>();

            foreach (var entity in entities)
            {
                var childs = Enumerable.Range(0, perCount).
                    Select(x => new ChildEntity { References = new Dictionary<string, object> { { "CoreEntityId", entity.Id } } });

                dict.Add(entity, childs);
            }

            var mergedValues = dict.Values.SelectMany(x => x);

            entities.MapEntityChildren<CoreEntity, ChildEntity>(mergedValues,
                x => x.CoreEntity,
                (entity, childs) => entity.Children = childs.ToList());


            foreach (var entity in entities)
            {
                var expected = dict[entity];

                AssertExtensions.FullEntityListExclusion(expected, entity.Children);
            }
        }

        #endregion

        #region MapEntityWithTypedIdChildren

        [Fact]
        public void MapEntityWithTypedIdChildren_Assigns_Entities_As_Expected_With_Int_Id()
        {
            // Need to demonstrate that we can bring in an IEnumerable source and children
            // Children will have references pointing to source objects
            // Source objects should get child mappings
            // Dictionary is probably your best bet

            const int entityCount = 4;
            const int perCount = 2;

            var entities = Enumerable.Range(0, entityCount).
                Select(x => new CoreEntity { Id = x }.GenerateForTest()).
                ToList();

            Dictionary<CoreEntity, IEnumerable<ChildEntity>> dict =
                new Dictionary<CoreEntity, IEnumerable<ChildEntity>>();

            foreach (var entity in entities)
            {
                var childs = Enumerable.Range(0, perCount).
                    Select(x => new ChildEntity { References = new Dictionary<string, object> { { "CoreEntityId", entity.Id } } });

                dict.Add(entity, childs);
            }

            var mergedValues = dict.Values.SelectMany(x => x);

            entities.MapEntityWithTypedIdChildren<CoreEntity, ChildEntity, int>(mergedValues,
                x => x.CoreEntity,
                (entity, childs) => entity.Children = childs.ToList());


            foreach (var entity in entities)
            {
                var expected = dict[entity];

                AssertExtensions.FullEntityListExclusion(expected, entity.Children);
            }
        }


        [Fact]
        public void MapEntityWithTypedIdChildren_Assigns_Entities_As_Expected_With_String_Id()
        {
            // Need to demonstrate that we can bring in an IEnumerable source and children
            // Children will have references pointing to source objects
            // Source objects should get child mappings
            // Dictionary is probably your best bet

            const int entityCount = 4;

            var entities = Enumerable.Range(0, entityCount).
                Select(x => new FlippedStringEntity { Id = x.ToString() }.GenerateForTest()).
                ToList();

            Dictionary<FlippedStringEntity, CoreEntity> dict =
                new Dictionary<FlippedStringEntity, CoreEntity>();

            foreach (var entity in entities)
            {
                var child = new CoreEntity().GenerateForTest();

                child.References = new Dictionary<string, object> { { "FlippedStringEntityId",  entity.Id } };

                dict.Add(entity, child);
            }

            var mergedValues = dict.Values;

            entities.MapEntityWithTypedIdChildren<FlippedStringEntity, CoreEntity, string>(mergedValues,
                x => x.FlippedStringEntity,
                (entity, childs) => entity.CoreEntity = childs.FirstOrDefault());

            foreach (var entity in entities)
            {
                var expected = dict[entity];

                Assert.Equal(expected, entity.CoreEntity);
            }
        }

        #endregion

        #region MapEntityParents

        [Fact]
        public void MapEntityParents_Assigns_Entities_As_Expected()
        {
            // Need to demonstrate that we can bring in an IEnumerable source and children
            // Children will have references pointing to source objects
            // Source objects should get child mappings
            // Dictionary is probably your best bet

            const int entityCount = 4;
            const int perCount = 2;

            var entities = Enumerable.Range(0, entityCount).
                Select(x => new CoreEntity { Id = x }.GenerateForTest()).
                ToList();

            Dictionary<CoreEntity, IEnumerable<ChildEntity>> dict =
                new Dictionary<CoreEntity, IEnumerable<ChildEntity>>();

            foreach (var entity in entities)
            {
                var childs = Enumerable.Range(0, perCount).
                    Select(x => new ChildEntity { References = new Dictionary<string, object> { { "CoreEntityId", entity.Id } } });

                dict.Add(entity, childs);
            }

            var children = dict.Values.SelectMany(x => x).ToList();

            children.MapEntityParents<ChildEntity, CoreEntity>(entities,
                y => y.CoreEntity,
                (child, core) => child.CoreEntity = core);


            foreach (var entity in children)
            {
                Assert.NotNull(entity.CoreEntity);

                var refId = entity.SafeGetIntRef<ChildEntity, CoreEntity>(y => y.CoreEntity);

                Assert.Equal(refId, entity.CoreEntity.Id);
            }
        }

        #endregion

        #region MapEntityWithTypedIdParents

        [Fact]
        public void MapEntityWithTypedIdParents_Assigns_Entities_As_Expected_With_Int_Id()
        {
            // Need to demonstrate that we can bring in an IEnumerable source and children
            // Children will have references pointing to source objects
            // Source objects should get child mappings
            // Dictionary is probably your best bet

            const int entityCount = 4;
            const int perCount = 2;

            var entities = Enumerable.Range(0, entityCount).
                Select(x => new CoreEntity { Id = x }.GenerateForTest()).
                ToList();

            Dictionary<CoreEntity, IEnumerable<ChildEntity>> dict =
                new Dictionary<CoreEntity, IEnumerable<ChildEntity>>();

            foreach (var entity in entities)
            {
                var childs = Enumerable.Range(0, perCount).
                    Select(x => new ChildEntity { References = new Dictionary<string, object> { { "CoreEntityId", entity.Id } } });

                dict.Add(entity, childs);
            }

            var children = dict.Values.SelectMany(x => x).ToList();

            children.MapEntityWithTypedIdParents<ChildEntity, int, CoreEntity, int>(entities,
                y => y.CoreEntity,
                (child, core) => child.CoreEntity = core);


            foreach (var entity in children)
            {
                Assert.NotNull(entity.CoreEntity);

                var refId = entity.SafeGetIntRef<ChildEntity, CoreEntity>(y => y.CoreEntity);

                Assert.Equal(refId, entity.CoreEntity.Id);
            }
        }


        [Fact]
        public void MapEntityWithTypedIdParents_Assigns_Entities_As_Expected_With_String_Id()
        {
            // Need to demonstrate that we can bring in an IEnumerable source and children
            // Children will have references pointing to source objects
            // Source objects should get child mappings
            // Dictionary is probably your best bet

            const int entityCount = 4;
            const int perCount = 2;

            var entities = Enumerable.Range(0, entityCount).
                Select(x => new CoreEntity { Id = x }.GenerateForTest()).
                ToList();

            Dictionary<CoreEntity, IEnumerable<FlippedStringEntity>> dict =
                new Dictionary<CoreEntity, IEnumerable<FlippedStringEntity>>();

            foreach (var entity in entities)
            {
                var childs = Enumerable.Range(0, perCount).
                    Select(x => new FlippedStringEntity { References = new Dictionary<string, object> { { "CoreEntityId", entity.Id } } });

                dict.Add(entity, childs);
            }

            var children = dict.Values.SelectMany(x => x).ToList();

            children.MapEntityWithTypedIdParents<FlippedStringEntity, string, CoreEntity, int>(entities,
                y => y.CoreEntity,
                (child, core) => child.CoreEntity = core);


            foreach (var entity in children)
            {
                Assert.NotNull(entity.CoreEntity);

                var refId = entity.SafeGetIntRef<FlippedStringEntity, CoreEntity>(y => y.CoreEntity);

                Assert.Equal(refId, entity.CoreEntity.Id);
            }
        }

        #endregion

        #endregion
    }
}
