using IBFramework.Domain;

namespace IBFramework.TestHelper.TestEntities.Base
{
    public class BaseTestEntity<TKey> : EntityWithTypedId<TKey>
    {
        public string Name { get; set; }

        public int Integer { get; set; }

        public decimal Decimal { get; set; } 
    }
}
