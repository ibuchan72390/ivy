using IBFramework.Data.Core.Interfaces.Base.Functional;
using IBFramework.Data.Core.Interfaces.Functional;

namespace IBFramework.Data.Core.Interfaces.Base.Blob
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
