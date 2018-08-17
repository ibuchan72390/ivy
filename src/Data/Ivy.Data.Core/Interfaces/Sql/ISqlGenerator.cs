using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.Sql;
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
        string GenerateGetQuery(string selectPrefix = null, string sqlWhere = null, string sqlJoin = null, string orderBy = null, int? limit = null, int? offset = null);

        string GenerateGetCountQuery(string sqlWhere = null, string sqlJoin = null);

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

        ISqlExecutionResult GenerateInsertQuery(TEntity entity, Dictionary<string, object> parms);

        ISqlExecutionResult GenerateInsertQuery(IEnumerable<TEntity> entity, Dictionary<string, object> parms);

        /*
         * UPDATE TEntity {sqlSet} WHERE {sqlWhere}
         */
        string GenerateUpdateQuery(string sqlSet, string sqlWhere = null);

        ISqlExecutionResult GenerateUpdateQuery(TEntity entity, Dictionary<string, object> parms, string sqlWhere = null);
    }

    public interface ISqlGenerator<TEntity, TKey> : ISqlGenerator<TEntity>
        where TEntity : IEntityWithTypedId<TKey>
    {
        // These will probably need to take params by reference somehow

        ISqlExecutionResult GenerateDeleteQuery(IEnumerable<TKey> idsToDelete, Dictionary<string, object> parms);

        ISqlExecutionResult GenerateDeleteQuery(TKey idToDelete, Dictionary<string, object> parms);

        ISqlExecutionResult GenerateGetQuery(IEnumerable<TKey> idsToDelete, Dictionary<string, object> parms);

        ISqlExecutionResult GenerateGetQuery(TKey idToGet, Dictionary<string, object> parms);

        ISqlExecutionResult GenerateSaveOrUpdateQuery(TEntity entity, Dictionary<string, object> parms);
    }
}
