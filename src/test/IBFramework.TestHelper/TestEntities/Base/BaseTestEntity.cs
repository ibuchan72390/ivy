using IBFramework.Core.Data.Domain;
using System;

namespace IBFramework.TestHelper.TestEntities.Base
{
    public class BaseTestEntity : IEquatable<BaseTestEntity>
    {
        public string Name { get; set; }

        public int Integer { get; set; }

        // Done for the 2 decimals placed on SQL DB
        private decimal _decimal;
        public decimal Decimal
        {
            get { return _decimal; }
            set { _decimal = Math.Round(value, 2); }
        }

        public double Double { get; set; }

        public bool Equals(BaseTestEntity other)
        {
            return (
                    Name.Equals(other.Name) &&
                    Integer.Equals(other.Integer) &&
                    Decimal.Equals(other.Decimal) &&
                    Double.Equals(other.Double)
                );
        }
    }

    public class BaseTestEntity<TKey> : BaseTestEntity, IEntityWithTypedId<TKey>, IEquatable<BaseTestEntity<TKey>>
    {
        public TKey Id { get; set; }

        public bool Equals(BaseTestEntity<TKey> other)
        {
            return base.Equals(other) && Id.Equals(other.Id);
        }
    }
}
