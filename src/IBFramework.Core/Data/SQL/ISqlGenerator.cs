using IBFramework.Core.Data.Domain;

namespace IBFramework.Core.Data.SQL
{
    public interface ISqlGenerator<TEntity>
    {
        string GenerateGetAllQuery();

        string GenerateDeleteAllQuery();
    }

    public interface ISqlGenerator<TEntity, TKey> : ISqlGenerator<TEntity>
        where TEntity : IEntityWithTypedId<TKey>
    {
        string GenerateDeleteQuery(TKey idToDelete);

        string GenerateGetQuery(TKey idToGet);

        string GenerateSaveOrUpdateQuery(TEntity entity);
    }
}
