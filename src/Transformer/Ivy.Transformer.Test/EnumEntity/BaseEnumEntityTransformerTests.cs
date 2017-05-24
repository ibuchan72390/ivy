using Ivy.Data.Core.Domain;
using Ivy.IoC;
using Ivy.Transformer.Core.Interfaces.EnumEntity;
using Ivy.Transformer.Core.Models;
using System.Linq;
using Xunit;

namespace Ivy.Transformer.Test.Base
{
    public class BaseEnumEntityTransformerTests : TransformerTestBase
    {
        #region Test Classes

        public enum TestEnum
        {
            Test1,
            Test2,
            Test3,
            Test4
        };

        public class TestEnumEntity : EnumEntity<TestEnum>
        {
        }

        public class TestEnumEntityModel : BaseEnumEntityModel
        {
        }

        #endregion

        #region Tests

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Model_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEnumEntityToViewModelTransformer<TestEnumEntity, TestEnumEntityModel>>();

            var entity = new TestEnumEntity { Id = 1, Name = "Name", FriendlyName = "FriendlyName" };

            var result = _sut.Transform(entity);

            AssertEntityEquality(entity, result);
        }

        [Fact]
        public void BaseTransformer_Converts_EntityWithTypedId_To_Model_List_As_Expected()
        {
            var _sut = ServiceLocator.Instance.Resolve<IEnumEntityToViewModelListTransformer<TestEnumEntity, TestEnumEntityModel>>();

            var entities = Enumerable.Range(0, 4).Select(x => new TestEnumEntity { Id = x, Name = $"Name{x}", FriendlyName = $"FriendlyName{x}" });

            var result = _sut.Transform(entities);

            foreach (var model in entities)
            {
                var targetModel = result.FirstOrDefault(x => x.Id == model.Id);
                AssertEntityEquality(model, targetModel);
            }
        }

        #endregion

        #region Helper Methods

        private void AssertEntityEquality(TestEnumEntity entity, TestEnumEntityModel result)
        {
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.FriendlyName, result.FriendlyName);
        }

        #endregion
    }
}
