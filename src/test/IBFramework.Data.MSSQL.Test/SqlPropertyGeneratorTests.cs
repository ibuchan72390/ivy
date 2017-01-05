using IBFramework.Core.Data.SQL;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestUtilities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IBFramework.Data.MSSQL.Test
{
    public class SqlPropertyGeneratorTests : MsSqlTestBase
    {
        #region Constructor

        public SqlPropertyGeneratorTests()
        {
            _sut = TestServiceLocator.StaticContainer.Resolve<ISqlPropertyGenerator>();
        }
        
        #endregion

        #region Variables & Constants

        private ISqlPropertyGenerator _sut;

        IList<string> baseExpectedAttrs = new List<string> { "Id", "Name", "Integer", "Decimal", "Double" };

        #endregion

        #region GetSqlPropertyNames

        [Fact]
        public void GetSqlPropertyNames_Properly_Returns_For_Basic_Entity()
        {
            var results = _sut.GetSqlPropertyNames<ParentEntity>();

            Assert.Empty(results.Except(baseExpectedAttrs));
            Assert.Empty(baseExpectedAttrs.Except(results));
        }

        [Fact]
        public void GetSqlPropertyNames_Properly_Returns_For_Entity_With_Foreign_Key_Object_Not_Ignored()
        {
            var localExpected = baseExpectedAttrs.Concat(new List<string> { "CoreEntityId" });

            var results = _sut.GetSqlPropertyNames<GuidIdEntity>();

            Assert.Empty(results.Except(localExpected));
            Assert.Empty(localExpected.Except(results));
        }

        [Fact]
        public void GetSqlPropertyNames_Properly_Handles_Ignored_Fk_Ids_And_Doesnt_Log_Collections_On_Complex_Objects()
        {
            var sut = TestServiceLocator.StaticContainer.Resolve<ISqlGenerator<CoreEntity>>();

            var localExpected = baseExpectedAttrs.Concat(new List<string> { "ParentEntityId" });

            var results = _sut.GetSqlPropertyNames<CoreEntity>();

            Assert.Empty(results.Except(localExpected));
            Assert.Empty(localExpected.Except(results));
        }

        #endregion
    }
}
