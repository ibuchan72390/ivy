using Ivy.TestHelper.TestEntities;
using Ivy.Utility.Core.Extensions;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class EntityExtensionsTests
    {
        [Fact]
        public void SafeGetIntRef_Properly_Returns_Id_For_Integer_Reference()
        {
            const int idVal = 123;

            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "ParentEntityId", idVal }
            };

            var result = entity.SafeGetIntRef<CoreEntity, ParentEntity>(x => x.ParentEntity);

            Assert.Equal(idVal, result);
        }

        [Fact]
        public void SafeGetStringRef_Properly_Returns_Id_For_String_Reference()
        {
            const string idVal = "TEST";

            var entity = new CoreEntity();
            entity.References = new Dictionary<string, object>
            {
                { "StringIdEntityId", idVal }
            };

            var result = entity.SafeGetStringRef<CoreEntity, StringEntity>(x => x.StringIdEntity);

            Assert.Equal(idVal, result);
        }
    }
}
