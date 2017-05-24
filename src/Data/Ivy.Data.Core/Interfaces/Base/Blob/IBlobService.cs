using Ivy.Data.Core.Interfaces.Init;

namespace Ivy.Data.Core.Interfaces.Base.Blob
{
    public interface IBlobService<TEntity, TRepo> : IInitialize
        where TEntity : class
        where TRepo : IBlobRepository<TEntity>
    {
    }
}
