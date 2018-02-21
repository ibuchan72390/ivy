﻿using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.EnumEntity;
using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using Ivy.Data.Core.Interfaces.Pagination;

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

        public virtual TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null)
        {
            return Repo.SaveOrUpdate(entity, tc);
        }

        public IEnumerable<TEntity> SaveOrUpdate(IEnumerable<TEntity> entities, ITranConn tc = null)
        {
            return Repo.SaveOrUpdate(entities, tc);
        }

        #endregion
    }
}
