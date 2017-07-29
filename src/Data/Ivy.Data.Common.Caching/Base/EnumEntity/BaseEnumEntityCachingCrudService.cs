using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.EnumEntity;
using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using Ivy.Data.Core.Interfaces.Pagination;
using Ivy.Caching.Core;

namespace Ivy.Data.Common.Caching.Base.EnumEntity
{
    public class BaseEnumEntityCachingCrudService<TEntity, TRepo, TEnum> :
        BaseEnumEntityCachingService<TEntity, TRepo, TEnum>,
        IEnumEntityCrudService<TEntity, TRepo, TEnum>

        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEnumEntityRepository<TEntity, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible

    {
        #region Constructor

        public BaseEnumEntityCachingCrudService(
            TRepo repo, 
            IObjectCache<IEnumerable<TEntity>> cache) 
            : base(repo, cache)
        {
        }

        #endregion

        #region Public Methods

        public void Delete(TEntity entity, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll(ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll(ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public IPaginationResponse<TEntity> GetAll(IPaginationRequest request, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetByIdList(IEnumerable<int> ids, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByName(TEnum name, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetByNames(IEnumerable<TEnum> enumVals, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
