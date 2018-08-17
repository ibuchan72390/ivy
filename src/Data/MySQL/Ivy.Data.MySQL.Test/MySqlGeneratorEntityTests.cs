using Ivy.Data.Core.Interfaces.SQL;
using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Data.MySQL.Test
{
    public class MySqlGeneratorEntityTests : 
        MySqlTestBase<ISqlPropertyGenerator>
    {
        #region Tests

        #region GenerateDeleteQuery

        [Fact]
        public void GenerateDeleteQuery_Generates_As_Expected_For_Single_Id()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity, int>>();

            const int idVal = 1;
            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateDeleteQuery(idVal, parms);

            string expected = $"DELETE FROM childentity WHERE `Id` = @entityId;";

            Assert.Equal(expected, result.Sql);
            Assert.True(result.Parms.Count == 1);
            Assert.Equal(idVal, result.Parms["@entityId"]);
        }

        [Fact]
        public void GenerateDeleteQuery_Generates_As_Expected_For_Many_Ids()
        {
            const int count = 3;

            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity, int>>();

            var idVals = Enumerable.Range(0, count).ToList();
            var parms = new Dictionary<string, object>();

            var idParams = idVals.Select(x => $"@id{x}");
            var idInList = string.Join(",", idParams);

            string expected = $"DELETE FROM childentity WHERE `Id` IN ({idInList});";

            var result = _sut.GenerateDeleteQuery(idVals, parms);

            Assert.Equal(expected, result.Sql);

            Assert.Equal(count, result.Parms.Count);

            foreach (var parm in result.Parms)
            {
                var val = (int)parm.Value;

                idVals.Remove(val);
            }

            Assert.Empty(idVals);
        }

        #endregion

        #region GenerateGetQuery (Entity)

        [Fact]
        public void GenerateGetQuery_Generates_As_Expected_For_Entity()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity, int>>();

            const int idVal = 1;
            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateGetQuery(idVal, parms);

            var attrs = Sut.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` WHERE `Id` = @entityId;";

            Assert.Equal(expected, result.Sql);
            Assert.True(result.Parms.Count == 1);
            Assert.Equal(idVal, result.Parms["@entityId"]);
        }

        #endregion

        #region GenerateGetQuery (Entities)

        [Fact]
        public void GenerateGetQuery_Generates_As_Expected_For_Entities()
        {
            const int count = 3;

            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity, int>>();

            var ids = Enumerable.Range(0, count).ToList();
            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateGetQuery(ids, parms);

            var idParams = ids.Select(x => $"@id{x}");
            var idInList = string.Join(",", idParams);

            var attrs = Sut.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` WHERE `Id` IN ({idInList});";

            Assert.Equal(expected, result.Sql);

            Assert.Equal(count, result.Parms.Count);

            foreach (var parm in result.Parms)
            {
                var val = (int)parm.Value;

                ids.Remove(val);
            }

            Assert.Empty(ids);
        }

        #endregion

        #region GenerateSaveOrUpdateQuery

        [Fact]
        public void GenerateSaveOrUpdateQuery_Generates_As_Expected_For_Save()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity, int>>();

            var childEntity = new ChildEntity().GenerateForTest();

            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateSaveOrUpdateQuery(childEntity, parms);

            var attrs = Sut.GetSqlPropertyNames<ChildEntity>().Where(x => x != "Id");

            var expectedAttrString = attrs.Select(FormatSqlAttr).Aggregate((x, y) => x + $", {y}");
            var expectedParamString = attrs.Aggregate("", (x, y) => $"{x}@{y}0, ");
            expectedParamString = expectedParamString.Substring(0, expectedParamString.Length - 2);

            var expected = $"INSERT INTO childentity ({expectedAttrString}) VALUES ({expectedParamString});SELECT LAST_INSERT_ID();";

            Assert.Equal(expected, result.Sql);
            Assert.True(result.Parms.Count == attrs.Count()); // param for each attr, not just id
        }

        [Fact]
        public void GenerateSaveOrUpdateQuery_Generates_As_Expected_For_Update()
        {
            ISqlGenerator<ChildEntity, int> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity, int>>();

            var childEntity = new ChildEntity { Id = 1 }.GenerateForTest();

            var parms = new Dictionary<string, object>();
            var result = _sut.GenerateSaveOrUpdateQuery(childEntity, parms);

            var allAttrs = Sut.GetSqlPropertyNames<ChildEntity>();
            var attrs = allAttrs.Where(x => x != "Id");

            var expectedParamString = attrs.Aggregate("", (x, y) => $"{x}`{y}` = @{y}, ");
            expectedParamString = expectedParamString.Substring(0, expectedParamString.Length - 2);

            var expected = $"UPDATE childentity SET {expectedParamString} WHERE `Id` = @entityId;";

            Assert.Equal(expected, result.Sql);
            Assert.True(result.Parms.Count == allAttrs.Count()); // param for each attr, not just id
        }

        #endregion

        #endregion
    }
}
