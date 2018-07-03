using Ivy.Data.Core.Interfaces.SQL;
using Ivy.IoC;
using Ivy.TestHelper.TestEntities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Data.Common.Test
{
    public class SqlPropertyGeneratorTests : 
        CommonDataTestBase<ISqlPropertyGenerator>
    {
        #region Variables & Constants

        IList<string> baseExpectedAttrs = new List<string> { "Id", "Name", "Integer", "Decimal", "Double", "Boolean" };

        #endregion

        #region GetSqlPropertyNames

        [Fact]
        public void GetSqlPropertyNames_Properly_Returns_For_Basic_Entity()
        {
            var results = Sut.GetSqlPropertyNames<ParentEntity>();

            Assert.Empty(results.Except(baseExpectedAttrs));
            Assert.Empty(baseExpectedAttrs.Except(results));
        }

        //[Fact]
        //public void GetSqlPropertyNames_Properly_Returns_For_Entity_With_Foreign_Key_Object_Not_Ignored()
        //{
        //    var localExpected = baseExpectedAttrs.Concat(new List<string> { "CoreEntityId" });

        //    var results = Sut.GetSqlPropertyNames<GuidEntity>();

        //    Assert.Empty(results.Except(localExpected));
        //    Assert.Empty(localExpected.Except(results));
        //}

        [Fact]
        public void GetSqlPropertyNames_Properly_Handles_Ignored_Fk_Ids_And_Doesnt_Log_Collections_On_Complex_Objects()
        {
            var sut = ServiceLocator.Instance.GetService<ISqlGenerator<CoreEntity>>();

            var localExpected = baseExpectedAttrs.Concat(new List<string> { "ParentEntityId", "FlippedStringEntityId", "WeirdAlternateIntegerId", "WeirdAlternateStringId" });

            var results = Sut.GetSqlPropertyNames<CoreEntity>();

            Assert.Empty(results.Except(localExpected));
            Assert.Empty(localExpected.Except(results));
        }

        #endregion
    }
}
