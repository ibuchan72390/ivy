using IBFramework.Core.Data.Domain;
using System.Collections.Generic;

namespace IBFramework.Core.Data.SQL
{
    public interface ISqlGenerator<TEntity>
    {
        /*
         * SELECT {selectPrefix} FROM TEntity WHERE {sqlWhere}
         * 
         * Simple select all if no params provided
         */
        string GenerateGetQuery(string selectPrefix = null, string sqlWhere = null);

        /*
         * DELETE FROM TEntity WHERE {sqlWhere}
         * 
         * Simple delete all if no params provided
         */
        string GenerateDeleteQuery(string sqlWhere = null);

        /*
         * INSERT INTO TEntity {sqlValue} VALUES {insertClause}
         * 
         * Wrap your own insert clause in () if you're trying to bulk insert
         * 
         */
        string GenerateInsertQuery(string sqlValue, string insertClause);

        string GenerateInsertQuery(TEntity entity, ref Dictionary<string, object> parms);

        string GenerateInsertQuery(IEnumerable<TEntity> entity, ref Dictionary<string, object> parms);

        /*
         * UPDATE TEntity {sqlSet} WHERE {sqlWhere}
         */
        string GenerateUpdateQuery(string sqlSet, string sqlWhere = null);

        string GenerateUpdateQuery(TEntity entity, ref Dictionary<string, object> parms, string sqlWhere = null);
    }

    public interface ISqlGenerator<TEntity, TKey> : ISqlGenerator<TEntity>
        where TEntity : IEntityWithTypedId<TKey>
    {
        // These will probably need to take params by reference somehow

        string GenerateDeleteQuery(TKey idToDelete, ref Dictionary<string, object> parms);

        string GenerateGetQuery(TKey idToGet, ref Dictionary<string, object> parms);

        string GenerateSaveOrUpdateQuery(TEntity entity, ref Dictionary<string, object> parms);
    }
}
