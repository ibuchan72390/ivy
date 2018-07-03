using Ivy.IoC;
using Ivy.TestHelper.TestEntities;
using Ivy.Web.Core.Json;
using Ivy.Web.Test.Base;
using Newtonsoft.Json;
using Xunit;

namespace Ivy.Web.Test.Json
{
    public class JsonManipulationServiceTests : 
        WebTestBase<IJsonManipulationService>
    {
        #region Tests

        #region ExtractJsonAttribute

        [Fact]
        public void ExtractJsonAttribute_Works_As_Expected_For_String()
        {
            const string attrName = "Name";
            const string name = "TEST";

            var entity = new CoreEntity { Name = name };

            var json = JsonConvert.SerializeObject(entity);

            var attr = Sut.ExtractJsonAttribute<string>(json, attrName);

            Assert.Equal(name, attr);
        }

        [Fact]
        public void ExtractJsonAttribute_Works_As_Expected_For_Int()
        {
            const string attrName = "Integer";
            const int integer = 123;

            var entity = new CoreEntity { Integer = integer };

            var json = JsonConvert.SerializeObject(entity);

            var attr = Sut.ExtractJsonAttribute<int>(json, attrName);

            Assert.Equal(integer, attr);
        }

        [Fact]
        public void ExtractJsonAttribute_Works_As_Expected_For_Object()
        {
            const string attrName = "ParentEntity";
            var parent = new ParentEntity
            {
                Id = 123,
                Double = 23.45,
                Integer = 234,
                Name = "TEST"
            };

            var entity = new CoreEntity { ParentEntity = parent };

            var json = JsonConvert.SerializeObject(entity);

            var attr = Sut.ExtractJsonAttribute<ParentEntity>(json, attrName);

            Assert.Equal(parent.Id, attr.Id);
            Assert.Equal(parent.Double, attr.Double);
            Assert.Equal(parent.Integer, attr.Integer);
            Assert.Equal(parent.Name, attr.Name);
        }

        #endregion

        #region RemoveJsonAttribute

        [Fact]
        public void RemoveJsonAttribute_Works_As_Expected()
        {
            var entity = new CoreEntity { Name = "TEST" };

            var json = JsonConvert.SerializeObject(entity);

            Assert.True(json.IndexOf("Name") > -1);

            json = Sut.RemoveJsonAttribute(json, "Name");

            Assert.True(json.IndexOf("Name") == -1);

            var result = JsonConvert.DeserializeObject<CoreEntity>(json);

            Assert.Null(result.Name);
        }

        #endregion

        #endregion
    }
}
