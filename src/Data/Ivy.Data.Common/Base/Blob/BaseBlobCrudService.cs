using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.Blob;
using System.Collections.Generic;

namespace Ivy.Data.Common.Base.Blob
{
    public class BaseBlobCrudService<TEntity, TRepo> :
        BaseBlobService<TEntity, TRepo>,
        IBlobCrudService<TEntity, TRepo>

        where TEntity : class
        where TRepo : IBlobRepository<TEntity>
    {
        #region Constructor

        public BaseBlobCrudService(TRepo repo) 
            : base(repo)
        {
        }

        #endregion

        #region Public Methods

        public virtual void BulkInsert(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            Repo.BulkInsert(entities, tc);
        }

        public virtual void DeleteAll(ITranConn tc = null)
        {
            Repo.DeleteAll(tc);
        }

        public virtual IEnumerable<TEntity> GetAll(ITranConn tc = null)
        {
            return Repo.GetAll(tc);
        }

        public virtual void Insert(TEntity entity, ITranConn tc = null)
        {
            Repo.Insert(entity, tc);
        }

        #endregion
    }
}
