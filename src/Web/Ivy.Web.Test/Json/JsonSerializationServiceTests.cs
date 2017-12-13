using Ivy.IoC;
using Ivy.TestHelper.TestEntities;
using Ivy.Web.Core.Json;
using Ivy.Web.Test.Base;
using Xunit;

namespace Ivy.Web.Test.Json
{
    public class JsonSerializationServiceTests : WebTestBase
    {
        #region Variables & Constants

        private IJsonSerializationService _sut;

        #endregion

        #region SetUp & TearDown

        public JsonSerializationServiceTests()
        {
            _sut = ServiceLocator.Instance.GetService<IJsonSerializationService>();
        }

        #endregion

        #region Tests

        [Fact]
        public void Serialize_Works_As_Expected()
        {
            var blobEntity = new BlobEntity
            {
                Name = "TestName",
                Integer = 789,
                Decimal = 123.45m,

                // Needs to be exact, seems JSON can round this out to a decimal with more places
                // No idea how that's happening, but it must be part of JSON conversion
                // Probably has to do with decimal v double data type
                Double = 234.987,
                Boolean = false
            };

            var result = _sut.Serialize(blobEntity);

            string expected = $"\"Name\":\"{blobEntity.Name}\",\"Integer\":{blobEntity.Integer},\"Decimal\":{blobEntity.Decimal},\"Double\":{blobEntity.Double},\"Boolean\":{blobEntity.Boolean.ToString().ToLower()},\"References\":null";

            expected = "{" + expected + "}";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Deserialize_Works_As_Expected()
        {
            const string name = "TestName";
            const int integer = 123;
            const decimal deci = 12.12m;
            const double dub = 23.456;

            string json = $"\"Name\":\"{name}\",\"Integer\":{integer},\"Decimal\":{deci},\"Double\":{dub},\"References\":null";

            json = "{" + json + "}";

            var result = _sut.Deserialize<BlobEntity>(json);

            Assert.Equal(name, result.Name);
            Assert.Equal(integer, result.Integer);
            Assert.Equal(deci, result.Decimal);
            Assert.Equal(dub, result.Double);
        }

        #endregion
    }
}
