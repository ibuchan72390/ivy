using IBFramework.Data.Core.Domain;
using IBFramework.Data.Core.Interfaces.Domain;
using System;

namespace IBFramework.TestHelper.TestEntities.Base
{
    public class BaseTestEntity : BaseEntityWithReferences, IBaseTestEntity
    {
        #region Variables & Constants

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

        public bool Equals(IBaseTestEntity other)
        {
            return (
                    Name.Equals(other.Name) &&
                    Integer.Equals(other.Integer) &&
                    Decimal.Equals(other.Decimal) &&
                    Double.Equals(other.Double)
                );
        }

        #endregion
    }

    public class BaseTestEntity<TKey> : BaseTestEntity, IBaseTestEntity<TKey>
    {
        #region Variables & Constants

        public TKey Id { get; set; }

        #endregion

        #region Public Methods

        public bool Equals(IBaseTestEntity<TKey> other)
        {
            return base.Equals(other) && Id.Equals(other.Id);
        }

        #endregion
    }

    public class BaseTestIntEntity : BaseTestEntity<int>, IEntity, IEquatable<BaseTestIntEntity>
    {
        public bool Equals(BaseTestIntEntity other)
        {
            return base.Equals(other as BaseTestEntity<int>);
        }
    }

}
