using Ivy.Data.Core.Interfaces.Base.Entity;
using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.Functional.EnumEntity;
using System;

namespace Ivy.Data.Core.Interfaces.Base.EnumEntity
{
    public interface IEnumEntityCrudService<TEntity, TRepo, TEnum> : 
        IEnumEntityService<TEntity, TRepo, TEnum>,
        IEntityCrudService<TEntity, TRepo>,
        IEnumEntityGet<TEntity, TEnum>

        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEnumEntityRepository<TEntity, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
