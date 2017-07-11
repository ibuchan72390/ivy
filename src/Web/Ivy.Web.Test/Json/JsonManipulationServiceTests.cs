using Ivy.IoC;
using Ivy.TestHelper.TestEntities;
using Ivy.Web.Core.Json;
using Ivy.Web.Test.Base;
using Newtonsoft.Json;
using Xunit;

namespace Ivy.Web.Test.Json
{
    public class JsonManipulationServiceTests : WebTestBase
    {
        #region Variables & Constants

        private IJsonManipulationService _sut;

        #endregion

        #region SetUp & TearDown

        public JsonManipulationServiceTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IJsonManipulationService>();
        }

        #endregion

        #region Tests
        
        [Fact]
        public void RemoveJsonAttribute_Works_As_Expected()
        {
            var entity = new CoreEntity { Name = "TEST" };

            var json = JsonConvert.SerializeObject(entity);

            Assert.True(json.IndexOf("Name") > -1);

            json = _sut.RemoveJsonAttribute(json, "Name");

            Assert.True(json.IndexOf("Name") == -1);

            var result = JsonConvert.DeserializeObject<CoreEntity>(json);

            Assert.Null(result.Name);
        }

        #endregion
    }
}
