using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.Entity;
using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Data.Common.Caching.Base.Entity
{
    public class BaseEntityCachingCrudService<TEntity, TRepo> :
        BaseEntityCachingService<TEntity, TRepo>,
        IEntityCrudService<TEntity, TRepo>

        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
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

        public TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }
    }
}
