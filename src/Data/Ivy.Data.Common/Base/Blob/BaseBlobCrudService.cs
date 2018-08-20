using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.Blob;
using System.Collections.Generic;
using Ivy.Data.Core.Interfaces.Pagination;
using System.Threading.Tasks;

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

        #region Insert

        public virtual void Insert(TEntity entity, ITranConn tc = null)
        {
            Repo.Insert(entity, tc);
        }

        public virtual async Task InsertAsync(TEntity entity, ITranConn tc = null)
        {
            await Repo.InsertAsync(entity, tc);
        }

        public virtual void BulkInsert(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            Repo.BulkInsert(entities, tc);
        }

        public virtual async Task BulkInsertAsync(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            await Repo.BulkInsertAsync(entities, tc);
        }

        #endregion

        #region Delete

        public virtual void DeleteAll(ITranConn tc = null)
        {
            Repo.DeleteAll(tc);
        }

        public virtual async Task DeleteAllAsync(ITranConn tc = null)
        {
            await Repo.DeleteAllAsync(tc);
        }

        #endregion

        #region Get

        public virtual IEnumerable<TEntity> GetAll(ITranConn tc = null)
        {
            return Repo.GetAll(tc);
        }

        public virtual IPaginationResponse<TEntity> GetAll(IPaginationRequest request, ITranConn tc = null)
        {
            return Repo.GetAll(request, tc);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(ITranConn tc = null)
        {
            return await Repo.GetAllAsync(tc);
        }

        public virtual async Task<IPaginationResponse<TEntity>> GetAllAsync(IPaginationRequest request, ITranConn tc = null)
        {
            return await Repo.GetAllAsync(request, tc);
        }

        #endregion

        #endregion
    }
}
