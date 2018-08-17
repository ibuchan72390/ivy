using Ivy.Data.Common.Base.Entity;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.Entity;
using Ivy.IoC.Core;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestValues;
using Xunit;
using Moq;

namespace Ivy.Data.Common.Test.Services
{
    public class BaseEntityServiceTests : CommonDataTestBase<IParentEntityTestService>
    {
        #region Variables & Constants

        private Mock<IEntityRepository<ParentEntity>> _mockRepo;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.RegisterSingleton<IParentEntityTestService, ParentEntityTestService>();

            _mockRepo = InitializeMoq<IEntityRepository<ParentEntity>>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public void InitializeByConnectionString_Executes_As_Expected()
        {
            var connString = MySqlTestValues.TestDbConnectionString;

            _mockRepo.Setup(x => x.InitializeByConnectionString(connString));

            Sut.InitializeByConnectionString(connString);

            _mockRepo.Verify(x => x.InitializeByConnectionString(connString), Times.Once);

            Assert.Equal(connString, Sut.ConnectionString);
        }

        [Fact]
        public void InitializeByDatabaseKey_Executes_As_Expected()
        {
            const string dbKey = "TESTING";
            var connString = MySqlTestValues.TestDbConnectionString;

            _mockRepo.Setup(x => x.InitializeByDatabaseKey(dbKey));
            _mockRepo.Setup(x => x.ConnectionString).Returns(connString);

            Sut.InitializeByDatabaseKey(dbKey);

            _mockRepo.Verify(x => x.InitializeByDatabaseKey(dbKey), Times.Once);
            _mockRepo.Verify(x => x.ConnectionString, Times.Once);

            Assert.Equal(connString, Sut.ConnectionString);
        }

        #endregion
    }

    public interface IParentEntityTestService : IEntityService<ParentEntity, IEntityRepository<ParentEntity>>
    {

    }

    public class ParentEntityTestService : BaseEntityService<ParentEntity, IEntityRepository<ParentEntity>>,
        IParentEntityTestService
    {
        public ParentEntityTestService(IEntityRepository<ParentEntity> repo) 
            : base(repo)
        {
        }
    }
}
