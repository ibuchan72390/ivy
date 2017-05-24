using Ivy.Data.Common.Sql;
using Ivy.Data.Core.Interfaces.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ivy.Data.MSSQL
{
    /*
     * This SQL Generator is going to make a few assumptions about how you design your database...
     * In order to use this SQL Generator in particular, you're going to need to make sure you follow
     * these procedures exactly...
     * 
     * 1) Tables are NOT pluralized, I've worked in an environment with pluralization and it's a stupid
     * waste of time and processing cycles.  Just name the Table what your object is named.
     * 
     * 2) Make all entity level collections into an IList<> for now.  In order for the child collections
     * to be skipped through the Entity mapper, we'll need to make them follow this pattern.
     */

    /*
     * Enhancing Performance
     * 1) Don't use SELECT * to ensure that we prevent the initial table scan for properties
     */

    public class MsSqlGenerator<TEntity> : BaseSqlGenerator<TEntity>, ISqlGenerator<TEntity>
    {
        #region Constructor

        public MsSqlGenerator(ISqlPropertyGenerator propertyGenerator)
            :base(propertyGenerator)
        {
        }

        #endregion

        #region Public Methods

        public string GenerateDeleteQuery(string sqlWhere = null)
        {
            var sql = AppendIfDefined($"DELETE FROM {_entityType.Name}", sqlWhere);
            return $"{sql};";
        }

        public string GenerateGetQuery(string selectPrefix = null, string sqlJoin = null, string sqlWhere = null, int? limit = null, int? offset = null)
        {
            var attributeNames = GeneratePropertyNameString(true, true);

            var sql = "SELECT ";

            if (selectPrefix != null)
            {
                sql = $"{sql}{selectPrefix} ";
            }

            sql += $"{attributeNames} FROM {_entityType.Name}";

            sql = AppendIfDefined(sql, sqlWhere);

            return $"{sql};";
        }

        public string GenerateInsertQuery(string sqlValue, string insertClause)
        {
            sqlValue = WrapInParenthesesIfNotWrapped(sqlValue);
            insertClause = WrapInParenthesesIfNotWrapped(insertClause);

            return $"INSERT INTO {_entityType.Name} {sqlValue} VALUES {insertClause};";
        }

        public string GenerateUpdateQuery(string sqlSet, string sqlWhere = null)
        {
            var sql =  $"UPDATE {_entityType.Name} SET {sqlSet}";

            sql = AppendIfDefined(sql, sqlWhere);

            return $"{sql};";
        }

        public string GenerateInsertQuery(TEntity entity, ref Dictionary<string, object> parms)
        {
            return GenerateInsertQuery(new List<TEntity> { entity }, ref parms);
        }

        public string GenerateInsertQuery(IEnumerable<TEntity> entities, ref Dictionary<string, object> parms)
        {
            var sb = new StringBuilder();

            var entityList = entities.ToList();

            var _propertyNames = GetPropertyNames(true).ToList();

            for (var y = 0; y < entityList.Count; y++)
            {
                for (var x = 0; x < _propertyNames.Count; x++)
                {
                    // Setup the SQL Insert
                    if (x == 0)
                        sb.Append("(");

                    var currentPropName = _propertyNames[x];
                    var currentParamKey = $"{currentPropName}{y}";

                    sb.Append($"@{currentParamKey}");

                    if (x < _propertyNames.Count - 1)
                        sb.Append(", ");
                    else
                        sb.Append(")");

                    // Setup the parameters
                    var targetProp = _entityType.GetProperty(currentPropName);

                    if (parms.ContainsKey(currentParamKey))
                    {
                        throw new Exception($"Key already in property dictionary! Key: {currentParamKey}");
                    }

                    parms.Add(currentParamKey, targetProp.GetValue(entityList[y]));
                }

                if (y < entityList.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            var sqlValueString = $"({GeneratePropertyNameString(false, false)})";

            return GenerateInsertQuery(sqlValueString, sb.ToString());
        }

        public string GenerateUpdateQuery(string sqlSet, string sqlWhere = null, object parms = null)
        {
            string sql = $"UPDATE {_entityType.Name} SET {sqlSet}";

            return $"{AppendIfDefined(sql, sqlWhere)};";
        }

        #endregion

        #region Abstract Methods

        protected override string FormatPropertyName(string propertyName)
        {
            return $"[{propertyName}]";
        }

        public string GenerateUpdateQuery(TEntity entity, ref Dictionary<string, object> parms, string sqlWhere = null)
        {
            throw new NotImplementedException();
        }

        public string GenerateGetQuery(string selectPrefix = null, string sqlWhere = null, string sqlJoin = null)
        {
            throw new NotImplementedException();
        }

        public string GenerateGetCountQuery()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
