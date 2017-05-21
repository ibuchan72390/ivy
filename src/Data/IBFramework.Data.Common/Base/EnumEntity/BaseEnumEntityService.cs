using IBFramework.Data.Common.Base.Entity;
using IBFramework.Data.Core.Interfaces;
using IBFramework.Data.Core.Interfaces.Base.EnumEntity;
using IBFramework.Data.Core.Interfaces.Domain;
using System;

namespace IBFramework.Data.Common.Base.EnumEntity
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
