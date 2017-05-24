using Ivy.Data.Common.Base.Entity;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.EnumEntity;
using Ivy.Data.Core.Interfaces.Domain;
using System;

namespace Ivy.Data.Common.Base.EnumEntity
{
    public class BaseEnumEntityService<TEntity, TRepo, TEnum> :
        BaseEntityService<TEntity, TRepo>,
        IEnumEntityService<TEntity, TRepo, TEnum>

        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEnumEntityRepository<TEntity, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        #region Constructor

        public BaseEnumEntityService(TRepo repo) 
            : base(repo)
        {
        }

        #endregion
    }
}
