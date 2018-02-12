using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.Entity;
using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;
using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Data.Common.Base.Entity
{
    public class BaseEntityCrudService<TEntity, TKey, TRepo> :
        BaseEntityService<TEntity, TKey, TRepo>,
        IEntityCrudService<TEntity, TKey, TRepo>

        where TEntity : class, IEntityWithTypedId<TKey>
        where TRepo : IEntityRepository<TEntity, TKey>
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

        public virtual void Delete(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            Repo.Delete(entities, tc);
        }

        public virtual void DeleteAll(ITranConn tc = null)
        {
            Repo.DeleteAll(tc);
        }

        public virtual void DeleteById(TKey id, ITranConn tc = null)
        {
            Repo.DeleteById(id, tc);
        }

        public virtual void DeleteByIdList(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            Repo.DeleteByIdList(ids, tc);
        }

        public virtual IEnumerable<TEntity> GetAll(ITranConn tc = null)
        {
            return Repo.GetAll(tc);
        }

        public virtual IPaginationResponse<TEntity> GetAll(IPaginationRequest request, ITranConn tc = null)
        {
            return Repo.GetAll(request, tc);
        }

        public virtual TEntity GetById(TKey id, ITranConn tc = null)
        {
            return Repo.GetById(id, tc);
        }

        public virtual IEnumerable<TEntity> GetByIdList(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            return Repo.GetByIdList(ids, tc);
        }

        public virtual TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null)
        {
            return Repo.SaveOrUpdate(entity, tc);
        }

        #endregion
    }

    public class BaseEntityCrudService<TEntity, TRepo> :
        BaseEntityCrudService<TEntity, int, TRepo>,
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
    }
}
