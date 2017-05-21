using IBFramework.Data.Core.Interfaces.Init;

namespace IBFramework.Data.Core.Interfaces.Base.Blob
{
    public interface IBlobService<TEntity, TRepo> : IInitialize
        where TEntity : class
        where TRepo : IBlobRepository<TEntity>
    {
    }
}
