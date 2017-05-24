using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.SQL
{
    public interface ISqlGenerator<TEntity>
    {
        /*
         * SQL Server Equivalent:
         * 
         *  SELECT *   -- <-- pick any columns here from your table, if you wanna exclude the RowNumber
         *  FROM (SELECT ROW_NUMBER OVER(ORDER BY ID DESC) RowNumber, * 
         *        FROM Reflow  
         *        WHERE ReflowProcessID = somenumber) t
         *  WHERE RowNumber >= 20 AND RowNumber <= 40    
         * 
         * 
         * MySQL Equivalent:
         * 
         * SELECT * FROM TEntity JOIN {sqlJoin} WHERE {sqlWhere} LIMIT {limit} OFFSET {offset}
         * 
         * 
         * Simple select all if no params provided
         */
        string GenerateGetQuery(string selectPrefix = null, string sqlWhere = null, string sqlJoin = null, int? limit = null, int? offset = null);

        string GenerateGetCountQuery();

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
