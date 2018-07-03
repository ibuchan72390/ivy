using Ivy.IoC;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Test.Entities;
using Ivy.Transformer.Test.Models;
using System.Linq;
using Xunit;

namespace Ivy.Transformer.Test.Base
{
    public class BaseEntityTransformerTests : TransformerTestBase<IEntityTransformer<TestEntity, TestEntityModel>>
    {
        #region Tests

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Model_As_Expected()
        {
            var entity = new TestEntity { Id = 1 };

            var result = Sut.Transform(entity);

            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Model_List_As_Expected()
        {
            var entities = Enumerable.Range(0, 4).Select(x => new TestEntity { Id = x });

            var result = Sut.Transform(entities);

            foreach (var model in entities)
            {
                var targetModel = result.FirstOrDefault(x => x.Id == model.Id);
                Assert.NotNull(targetModel);
            }
        }

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Entity_As_Expected()
        {
            var model = new TestEntityModel { Id = 1 };

            var result = Sut.Transform(model);

            Assert.Equal(model.Id, result.Id);
        }

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Entity_List_As_Expected()
        {
            var models = Enumerable.Range(0, 4).Select(x => new TestEntityModel { Id = x });

            var result = Sut.Transform(models);

            foreach (var model in models)
            {
                var targetModel = result.FirstOrDefault(x => x.Id == model.Id);
                Assert.NotNull(targetModel);
            }
        }

        #endregion
    }
}
