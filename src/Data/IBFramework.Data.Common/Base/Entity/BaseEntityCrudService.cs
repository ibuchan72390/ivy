using IBFramework.Data.Core.Interfaces;
using IBFramework.Data.Core.Interfaces.Base.Entity;
using IBFramework.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace IBFramework.Data.Common.Base.Entity
{
    public class BaseEntityCrudService<TEntity, TRepo> :
        BaseEntityService<TEntity, TRepo>,
        IEntityCrudService<TEntity, TRepo>

        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
        #region Constructor

        public BaseEntityCrudService(TRepo repo) 
            : base(repo)
        {
        }

        #endregion

        #region Public Methods

        public virtual void Delete(TEntity entity, ITranConn tc = null)
        {
            Repo.Delete(entity, tc);
        }

        public virtual void DeleteAll(ITranConn tc = null)
        {
            Repo.DeleteAll(tc);
        }

        public virtual void DeleteById(int id, ITranConn tc = null)
        {
            Repo.DeleteById(id, tc);
        }

        public virtual IEnumerable<TEntity> GetAll(ITranConn tc = null)
        {
            return Repo.GetAll(tc);
        }

        public virtual TEntity GetById(int id, ITranConn tc = null)
        {
            return Repo.GetById(id, tc);
        }

        public virtual IEnumerable<TEntity> GetByIdList(IEnumerable<int> ids, ITranConn tc = null)
        {
            return Repo.GetByIdList(ids, tc);
        }

        public virtual TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null)
        {
            return Repo.SaveOrUpdate(entity, tc);
        }

        #endregion
    }
}
