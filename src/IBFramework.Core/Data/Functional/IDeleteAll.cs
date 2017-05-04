namespace IBFramework.Core.Data.Functional
{
    public interface IDeleteAll<TEntity>
        where TEntity : class
    {
        void DeleteAll(ITranConn tc = null);
    }
}
