using IBFramework.Core.Data.Domain;

namespace IBFramework.TestHelper.TestEntities.Base
{
    public class BaseTestEntity
    {
        public string Name { get; set; }

        public int Integer { get; set; }

        public decimal Decimal { get; set; }

        public double Double { get; set; }
    }

    public class BaseTestEntity<TKey> : BaseTestEntity, IEntityWithTypedId<TKey>
    {
        public TKey Id { get; set; }
    }
}
