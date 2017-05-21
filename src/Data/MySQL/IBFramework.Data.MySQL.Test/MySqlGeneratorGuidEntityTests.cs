//using IBFramework.Core.Data.SQL;
//using IBFramework.IoC;
//using IBFramework.TestHelper;
//using IBFramework.TestHelper.TestEntities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace IBFramework.Data.MySQL.Test
//{
//    public class MySqlGeneratorGuidEntityTests : MySqlTestBase
//    {
//        #region Variables & Constants

//        private ISqlPropertyGenerator _propertyGenerator;

//        #endregion

//        #region Constructor

//        public MySqlGeneratorGuidEntityTests()
//        {
//            _propertyGenerator = ServiceLocator.Instance.Resolve<ISqlPropertyGenerator>();
//        }

//        #endregion

//        #region Tests

//        #region GenerateDeleteQuery

//        [Fact]
//        public void GenerateDeleteQuery_Generates_As_Expected()
//        {
//            ISqlGenerator<GuidEntity, Guid> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<GuidEntity, Guid>>();

//            Guid idVal = Guid.NewGuid();
//            var parms = new Dictionary<string, object>();
//            var result = _sut.GenerateDeleteQuery(idVal, ref parms);

//            var attrs = _propertyGenerator.GetSqlPropertyNames<GuidEntity>().Select(FormatSqlAttr);

//            var expectedAttrString = attrs.Aggregate((x, y) => x + $", {y}");

//            string expected = $"DELETE FROM GuidIdEntity WHERE `Id` = @entityId;";

//            Assert.Equal(expected, result);
//            Assert.True(parms.Count == 1);
//            Assert.Equal(idVal.ToString(), parms["@entityId"]);
//        }

//        #endregion

//        #region GenerateGetQuery

//        [Fact]
//        public void GenerateGetQuery_Generates_As_Expected()
//        {
//            ISqlGenerator<GuidEntity, Guid> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<GuidEntity, Guid>>();

//            Guid idVal = Guid.NewGuid();
//            var parms = new Dictionary<string, object>();
//            var result = _sut.GenerateGetQuery(idVal, ref parms);

//            var attrs = _propertyGenerator.GetSqlPropertyNames<GuidEntity>().Select(FormatSqlAttr);

//            var expectedAttrString = attrs.
//                                        Select(x => $"`THIS`.{x}").
//                                        Aggregate((x, y) => x + $", {y}");

//            string expected = $"SELECT {expectedAttrString} FROM GuidIdEntity `THIS` WHERE `Id` = @entityId;";

//            Assert.Equal(expected, result);
//            Assert.True(parms.Count == 1);
//            Assert.Equal(idVal.ToString(), parms["@entityId"]);
//        }

//        #endregion

//        #region GenerateSaveOrUpdateQuery

//        [Fact]
//        public void GenerateSaveOrUpdateQuery_Generates_As_Expected_For_Save()
//        {
//            ISqlGenerator<GuidEntity, Guid> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<GuidEntity, Guid>>();

//            var GuidIdEntity = new GuidEntity().GenerateForTest();

//            var parms = new Dictionary<string, object>();
//            var result = _sut.GenerateSaveOrUpdateQuery(GuidIdEntity, ref parms);

//            var attrs = _propertyGenerator.GetSqlPropertyNames<GuidEntity>();

//            var expectedAttrString = attrs.Select(FormatSqlAttr).Aggregate((x, y) => x + $", {y}");
//            var expectedParamString = attrs.Aggregate("", (x, y) => $"{x}@{y}0, ");
//            expectedParamString = expectedParamString.Substring(0, expectedParamString.Length - 2);

//            var expected = $"REPLACE INTO GuidIdEntity ({expectedAttrString}) VALUES ({expectedParamString});";

//            Assert.Equal(expected, result);
//            Assert.True(parms.Count == attrs.Count()); // param for each attr, not just id
//        }

//        #endregion

//        #endregion
//    }
//}
