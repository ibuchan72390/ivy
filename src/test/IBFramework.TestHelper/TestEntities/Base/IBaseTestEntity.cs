using IBFramework.Core.Data.Domain;
using System;

namespace IBFramework.TestHelper.TestEntities.Base
{
    public interface IBaseTestEntity : IEquatable<IBaseTestEntity>
    {
        string Name { get; set; }

        int Integer { get; set; }

        decimal Decimal { get; set; }

        double Double { get; set; }
    }

    public interface IBaseTestEntity<TKey> : IBaseTestEntity, IEntityWithTypedId<TKey>, IEquatable<IBaseTestEntity<TKey>>
    {

    }

}
