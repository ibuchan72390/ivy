﻿using Ivy.Data.Core.Interfaces.Domain;

namespace Ivy.Data.Core.Interfaces.Functional.Entity
{
    public interface IEntitySaveOrUpdate<TEntity, TKey> 
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null);
    }

    public interface IEntitySaveOrUpdate<TEntity> : IEntitySaveOrUpdate<TEntity, int>
        where TEntity : class, IEntity
    {
    }
}