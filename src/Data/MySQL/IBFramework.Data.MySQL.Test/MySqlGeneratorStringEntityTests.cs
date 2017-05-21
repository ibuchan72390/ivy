using IBFramework.Data.Core.Interfaces.SQL;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using IBFramework.Utility.Core;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IBFramework.Data.MySQL.Test
{
    public class MySqlGeneratorStringEntityTests : MySqlTestBase
    {
        #region Variables & Constants

        private ISqlPropertyGenerator _propertyGenerator;

        private IRandomizationHelper _randomizationHelper;

        #endregion

        #region Constructor

        public MySqlGeneratorStringEntityTests()
        {
            _propertyGenerator = ServiceLocator.Instance.Resolve<ISqlPropertyGenerator>();

            _randomizationHelper = ServiceLocator.Instance.Resolve<IRandomizationHelper>();
        }

        #endregion

        #region Tests

        #region GenerateDeleteQuery

        [Fact]
        public void GenerateDeleteQuery_Generates_As_Expected()
        {
            ISqlGenerator<StringEntity, string> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<StringEntity, string>>();

            string idVal = _randomizationHelper.RandomString();
            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateDeleteQuery(idVal, ref parms);

            var attrs = _propertyGenerator.GetSqlPropertyNames<StringEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.Aggregate((x, y) => x + $", {y}");

            string expected = $"DELETE FROM stringentity WHERE `Id` = @entityId;";

            Assert.Equal(expected, result);
            Assert.True(parms.Count == 1);
            Assert.Equal(idVal, parms["@entityId"]);
        }

        #endregion

        #region GenerateGetQuery

        [Fact]
        public void GenerateGetQuery_Generates_As_Expected()
        {
            ISqlGenerator<StringEntity, string> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<StringEntity, string>>();

            string idVal = _randomizationHelper.RandomString();
            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateGetQuery(idVal, ref parms);

            var attrs = _propertyGenerator.GetSqlPropertyNames<StringEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM stringentity `THIS` WHERE `Id` = @entityId;";

            Assert.Equal(expected, result);
            Assert.True(parms.Count == 1);
            Assert.Equal(idVal, parms["@entityId"]);
        }

        #endregion

        #region GenerateSaveOrUpdateQuery

        [Fact]
        public void GenerateSaveOrUpdateQuery_Generates_As_Expected_For_Save()
        {
            ISqlGenerator<StringEntity, string> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<StringEntity, string>>();

            var StringIdEntity = new StringEntity().GenerateForTest();

            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateSaveOrUpdateQuery(StringIdEntity, ref parms);

            var attrs = _propertyGenerator.GetSqlPropertyNames<StringEntity>();

            var expectedAttrString = attrs.Select(FormatSqlAttr).Aggregate((x, y) => x + $", {y}");
            var expectedParamString = attrs.Aggregate("", (x, y) => $"{x}@{y}0, ");
            expectedParamString = expectedParamString.Substring(0, expectedParamString.Length - 2);

            var expected = $"REPLACE INTO stringentity ({expectedAttrString}) VALUES ({expectedParamString});";

            Assert.Equal(expected, result);
            Assert.True(parms.Count == attrs.Count()); // param for each attr, not just id
        }

        #endregion

        #endregion
    }
}
