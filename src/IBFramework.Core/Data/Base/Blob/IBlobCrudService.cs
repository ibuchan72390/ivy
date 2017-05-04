using IBFramework.Core.Data.Base.Functional;
using IBFramework.Core.Data.Functional;

namespace IBFramework.Core.Data.Base.Blob
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
