using Ivy.IoC;
using Ivy.TestHelper.TestEntities;
using Ivy.Transformer.Core.Interfaces.EnumEntity;
using Ivy.Transformer.Test.Models;
using System.Linq;
using Xunit;

namespace Ivy.Transformer.Test.Base
{
    public class BaseEnumEntityTransformerTests : TransformerTestBase
    {
        #region Tests

        #region Entity -> VM

        [Fact]
        public void BaseTransformer_Converts_EnumEntity_To_Model_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEnumEntityTransformer<TestEnumEntity, TestEnumEntityModel>>();

            var entity = new TestEnumEntity { Id = 1, Name = "Name", FriendlyName = "FriendlyName", SortOrder = 1 };

            var result = _sut.Transform(entity);

            AssertEntityEquality(entity, result, true);
        }

        [Fact]
        public void BaseTransformer_Converts_EnumEntity_To_Model_List_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEnumEntityTransformer<TestEnumEntity, TestEnumEntityModel>>();

            var entities = Enumerable.Range(0, 4).Select(x => new TestEnumEntity { Id = x, Name = $"Name{x}", FriendlyName = $"FriendlyName{x}", SortOrder = x });

            var results = _sut.Transform(entities);

            foreach (var model in entities)
            {
                var targetModel = results.FirstOrDefault(x => x.Id == model.Id);
                AssertEntityEquality(model, targetModel, true);
            }

            for (var i = 0; i < results.Count(); i++)
            {
                var target = results.ElementAt(i);
                Assert.Equal(i, target.SortOrder);
            }
        }

        #endregion

        #region VM -> Entity

        [Fact]
        public void BaseTransformer_Converts_Model_To_EnumEntity_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEnumEntityTransformer<TestEnumEntity, TestEnumEntityModel>>();

            var model = new TestEnumEntityModel { Id = 1, Name = "Name", FriendlyName = "FriendlyName", SortOrder = 1 };

            var result = _sut.Transform(model);

            AssertEntityEquality(result, model, false);
        }

        [Fact]
        public void BaseTransformer_Converts_Model_To_EnumEntity_List_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEnumEntityTransformer<TestEnumEntity, TestEnumEntityModel>>();

            var models = Enumerable.Range(0, 4).Select(x => new TestEnumEntityModel { Id = x, Name = $"Name{x}", FriendlyName = $"FriendlyName{x}", SortOrder = x });

            var results = _sut.Transform(models);

            foreach (var model in models)
            {
                var result = results.FirstOrDefault(x => x.Id == model.Id);
                AssertEntityEquality(result, model, false);
            }
        }

        #endregion

        #endregion

        #region Helper Methods

        private void AssertEntityEquality(TestEnumEntity entity, TestEnumEntityModel result, bool validateSort)
        {
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.FriendlyName, result.FriendlyName);

            if (validateSort)
                Assert.Equal(entity.SortOrder, result.SortOrder);
        }

        #endregion
    }
}
