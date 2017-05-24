using Ivy.Data.Core.Interfaces.Base.Functional;
using Ivy.Data.Core.Interfaces.Functional;

namespace Ivy.Data.Core.Interfaces.Base.Blob
{
    public interface IBlobCrudService<TEntity, TRepo> : 
        IBlobService<TEntity, TRepo>,
        IGetAll<TEntity>,
        IDeleteAll<TEntity>,
        IBlobInsert<TEntity>
        
        where TEntity : class
        where TRepo : IBlobRepository<TEntity>
    {
    }
}
