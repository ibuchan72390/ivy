namespace Ivy.Data.Core.Interfaces.Functional
{
    public interface IDeleteAll<TEntity>
        where TEntity : class
    {
        void DeleteAll(ITranConn tc = null);
    }
}
