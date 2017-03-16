using IBFramework.Core.Data.Init;

namespace IBFramework.Core.Data.Base
{
    public interface IBlobService<TEntity, TRepo> : IInitialize
        where TEntity : class
        where TRepo : IBlobRepository<TEntity>
    {
    }
}
