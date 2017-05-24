using Ivy.Data.Core.Domain;
using Ivy.IoC;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Core.Models;
using System.Linq;
using Xunit;

namespace Ivy.Transformer.Test.Base
{
    public class BaseEntityTransformerTests : TransformerTestBase
    {
        #region Test Classes

        public class TestEntity : Entity
        {
        }

        public class TestEntityModel : BaseEntityModel
        {
        }

        #endregion

        #region Tests

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Model_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEntityToViewModelTransformer<TestEntity, TestEntityModel>>();

            var entity = new TestEntity { Id = 1 };

            var result = _sut.Transform(entity);

            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Model_List_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEntityToViewModelListTransformer<TestEntity, TestEntityModel>>();

            var entities = Enumerable.Range(0, 4).Select(x => new TestEntity { Id = x });

            var result = _sut.Transform(entities);

            foreach (var model in entities)
            {
                var targetModel = result.FirstOrDefault(x => x.Id == model.Id);
                Assert.NotNull(targetModel);
            }
        }

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Entity_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IViewModelToEntityTransformer<TestEntity, TestEntityModel>>();

            var model = new TestEntityModel { Id = 1 };

            var result = _sut.Transform(model);

            Assert.Equal(model.Id, result.Id);
        }

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Entity_List_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IViewModelToEntityListTransformer<TestEntity, TestEntityModel>>();

            var models = Enumerable.Range(0, 4).Select(x => new TestEntityModel { Id = x });

            var result = _sut.Transform(models);

            foreach (var model in models)
            {
                var targetModel = result.FirstOrDefault(x => x.Id == model.Id);
                Assert.NotNull(targetModel);
            }
        }

        #endregion
    }
}
