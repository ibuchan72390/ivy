using IBFramework.Data.Core.Interfaces.SQL;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IBFramework.Data.MySQL.Test
{
    public class MySqlGeneratorEntityTests : MySqlTestBase
    {
        #region Variables & Constants

        private ISqlPropertyGenerator _propertyGenerator;

        #endregion

        #region Constructor

        public MySqlGeneratorEntityTests()
        {
            _propertyGenerator = ServiceLocator.Instance.Resolve<ISqlPropertyGenerator>();
        }

        #endregion

        #region Tests

        #region GenerateDeleteQuery

        [Fact]
        public void GenerateDeleteQuery_Generates_As_Expected()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity, int>>();

            const int idVal = 1;
            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateDeleteQuery(idVal, ref parms);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.Aggregate((x, y) => x + $", {y}");

            string expected = $"DELETE FROM childentity WHERE `Id` = @entityId;";

            Assert.Equal(expected, result);
            Assert.True(parms.Count == 1);
            Assert.Equal(idVal, parms["@entityId"]);
        }

        #endregion

        #region GenerateGetQuery

        [Fact]
        public void GenerateGetQuery_Generates_As_Expected()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity, int>>();

            const int idVal = 1;
            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateGetQuery(idVal, ref parms);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` WHERE `Id` = @entityId;";

            Assert.Equal(expected, result);
            Assert.True(parms.Count == 1);
            Assert.Equal(idVal, parms["@entityId"]);
        }

        #endregion

        #region GenerateSaveOrUpdateQuery

        [Fact]
        public void GenerateSaveOrUpdateQuery_Generates_As_Expected_For_Save()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity, int>>();

            var childEntity = new ChildEntity().GenerateForTest();

            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateSaveOrUpdateQuery(childEntity, ref parms);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Where(x => x != "Id");

            var expectedAttrString = attrs.Select(FormatSqlAttr).Aggregate((x, y) => x + $", {y}");
            var expectedParamString = attrs.Aggregate("", (x, y) => $"{x}@{y}0, ");
            expectedParamString = expectedParamString.Substring(0, expectedParamString.Length - 2);

            var expected = $"INSERT INTO childentity ({expectedAttrString}) VALUES ({expectedParamString});SELECT LAST_INSERT_ID();";

            Assert.Equal(expected, result);
            Assert.True(parms.Count == attrs.Count()); // param for each attr, not just id
        }

        [Fact]
        public void GenerateSaveOrUpdateQuery_Generates_As_Expected_For_Update()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity, int>>();

            var childEntity = new ChildEntity().SaveForTest();

            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateSaveOrUpdateQuery(childEntity, ref parms);

            var allAttrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>();
            var attrs = allAttrs.Where(x => x != "Id");

            var expectedParamString = attrs.Aggregate("", (x, y) => $"{x}`{y}` = @{y}, ");
            expectedParamString = expectedParamString.Substring(0, expectedParamString.Length - 2);

            var expected = $"UPDATE childentity SET {expectedParamString} WHERE `Id` = @entityId;";

            Assert.Equal(expected, result);
            Assert.True(parms.Count == allAttrs.Count()); // param for each attr, not just id
        }

        #endregion

        #endregion
    }
}
