using IBFramework.Data.Core.Interfaces;
using IBFramework.Data.Core.Interfaces.Base.EnumEntity;
using IBFramework.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;

namespace IBFramework.Data.Common.Base.EnumEntity
{
    public class BaseEnumEntityCrudService<TEntity, TRepo, TEnum> :
        BaseEnumEntityService<TEntity, TRepo, TEnum>,
        IEnumEntityCrudService<TEntity, TRepo, TEnum>

        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEnumEntityRepository<TEntity, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        #region Constructor

        public BaseEnumEntityCrudService(TRepo repo) 
            : base(repo)
        {
        }

        #endregion

        #region Public Methods

        public virtual void Delete(TEntity entity, ITranConn tc = null)
        {
            Repo.Delete(entity, tc);
        }

        public virtual void DeleteById(int id, ITranConn tc = null)
        {
            Repo.DeleteById(id, tc);
        }

        public virtual TEntity GetById(int id, ITranConn tc = null)
        {
            return Repo.GetById(id, tc);
        }

        public virtual IEnumerable<TEntity> GetByIdList(IEnumerable<int> ids, ITranConn tc = null)
        {
            return Repo.GetByIdList(ids, tc);
        }

        public virtual TEntity GetByName(TEnum name, ITranConn tc = null)
        {
            return Repo.GetByName(name, tc);
        }

        public virtual TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null)
        {
            return Repo.SaveOrUpdate(entity, tc);
        }

        #endregion
    }
}
