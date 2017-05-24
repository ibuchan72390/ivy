using Ivy.Data.Common.Pagination;
using Ivy.IoC;
using Ivy.TestUtilities;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Core.Interfaces.Pagination;
using Ivy.Transformer.Test.Base;
using Ivy.Transformer.Test.Entities;
using Ivy.Transformer.Test.Models;
using System.Linq;
using Xunit;

namespace Ivy.Transformer.Test.Pagination
{
    public class PaginatedResponseTransformerTests : TransformerTestBase
    {
        #region Variables & Constants

        private readonly IPaginatedResponseTransformer<TestEntity, TestEntityModel, IEntityToViewModelListTransformer<TestEntity, TestEntityModel>> _sut;

        #endregion

        #region Constructor

        public PaginatedResponseTransformerTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IPaginatedResponseTransformer<TestEntity, TestEntityModel, IEntityToViewModelListTransformer<TestEntity, TestEntityModel>>>();
        }

        #endregion

        #region Tests

        [Fact]
        public void Transform_PaginationViewModel_Works_As_Expected()
        {
            const int totalCount = 20;
            var entities = Enumerable.Range(0, 5).Select(x => new TestEntity { Id = x });

            var model = new PaginationResponse<TestEntity>
            {
                Data = entities,
                TotalCount = totalCount
            };

            var result = _sut.Transform(model);

            Assert.Equal(totalCount, result.TotalCount);

            var entityIds = entities.Select(x => x.Id);
            var resultIds = result.Data.Select(x => x.Id);

            AssertExtensions.FullBasicListExclusion(entityIds, resultIds);
        }

        #endregion
    }
}
