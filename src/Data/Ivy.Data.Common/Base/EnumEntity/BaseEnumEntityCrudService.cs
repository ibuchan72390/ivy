using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.EnumEntity;
using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using Ivy.Data.Core.Interfaces.Pagination;
using System.Threading.Tasks;

namespace Ivy.Data.Common.Base.EnumEntity
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

        #region Delete

        #region Synchronous

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
            Repo.DeleteAll();
        }

        public virtual void DeleteById(int id, ITranConn tc = null)
        {
            Repo.DeleteById(id, tc);
        }

        public virtual void DeleteByIdList(IEnumerable<int> ids, ITranConn tc = null)
        {
            Repo.DeleteByIdList(ids, tc);
        }

        #endregion

        #region Asynchronous

        public virtual async Task DeleteAsync(TEntity entity, ITranConn tc = null)
        {
            await Repo.DeleteAsync(entity, tc);
        }

        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            await Repo.DeleteAsync(entities, tc);
        }

        public virtual async Task DeleteAllAsync(ITranConn tc = null)
        {
            await Repo.DeleteAllAsync();
        }

        public virtual async Task DeleteByIdAsync(int id, ITranConn tc = null)
        {
            await Repo.DeleteByIdAsync(id, tc);
        }

        public virtual async Task DeleteByIdListAsync(IEnumerable<int> ids, ITranConn tc = null)
        {
            await Repo.DeleteByIdListAsync(ids, tc);
        }

        #endregion

        #endregion

        #region Get

        #region Synchronous

        public virtual IEnumerable<TEntity> GetAll(ITranConn tc = null)
        {
            return Repo.GetAll(tc);
        }

        public virtual IPaginationResponse<TEntity> GetAll(IPaginationRequest request, ITranConn tc = null)
        {
            return Repo.GetAll(request, tc);
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

        public virtual IEnumerable<TEntity> GetByNames(IEnumerable<TEnum> enumVals, ITranConn tc = null)
        {
            return Repo.GetByNames(enumVals, tc);
        }

        #endregion

        #region Asynchronous

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(ITranConn tc = null)
        {
            return await Repo.GetAllAsync(tc);
        }

        public virtual async Task<IPaginationResponse<TEntity>> GetAllAsync(IPaginationRequest request, ITranConn tc = null)
        {
            return await Repo.GetAllAsync(request, tc);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, ITranConn tc = null)
        {
            return await Repo.GetByIdAsync(id, tc);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIdListAsync(IEnumerable<int> ids, ITranConn tc = null)
        {
            return await Repo.GetByIdListAsync(ids, tc);
        }

        public virtual async Task<TEntity> GetByNameAsync(TEnum name, ITranConn tc = null)
        {
            return await Repo.GetByNameAsync(name, tc);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByNamesAsync(IEnumerable<TEnum> enumVals, ITranConn tc = null)
        {
            return await Repo.GetByNamesAsync(enumVals, tc);
        }

        #endregion

        #endregion SaveOrUpdate

        #region SaveOrUpdate

        #region Synchronous

        public virtual TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null)
        {
            return Repo.SaveOrUpdate(entity, tc);
        }

        public virtual IEnumerable<TEntity> SaveOrUpdate(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            return Repo.SaveOrUpdate(entities, tc);
        }

        #endregion

        #region Asynchronous

        public virtual async Task<TEntity> SaveOrUpdateAsync(TEntity entity, ITranConn tc = null)
        {
            return await Repo.SaveOrUpdateAsync(entity, tc);
        }

        public virtual async Task<IEnumerable<TEntity>> SaveOrUpdateAsync(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            return await Repo.SaveOrUpdateAsync(entities, tc);
        }

        #endregion

        #endregion

        #endregion
    }
}
