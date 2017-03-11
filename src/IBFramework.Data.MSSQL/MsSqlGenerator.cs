﻿using IBFramework.Core.Data.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IBFramework.Data.MSSQL
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

    public class MsSqlGenerator<TEntity> : ISqlGenerator<TEntity>
    {
        #region Variables & Constants

        private readonly Type _entityType;
        private readonly ISqlPropertyGenerator _propertyGenerator;

        private IList<string> _propertyNames = null;

        #endregion

        #region Constructor

        public MsSqlGenerator(ISqlPropertyGenerator propertyGenerator)
        {
            _entityType = typeof(TEntity);
            _propertyGenerator = propertyGenerator;
        }

        #endregion

        #region Public Methods

        public string GenerateDeleteQuery(string sqlWhere = null)
        {
            var sql = AppendWhereIfDefined($"DELETE FROM {_entityType.Name}", sqlWhere);
            return $"{sql};";
        }

        public string GenerateGetQuery(string selectPrefix = null, string sqlWhere = null)
        {
            var attributeNames = GeneratePropertyNameString();

            var sql = "SELECT ";

            if (selectPrefix != null)
            {
                sql = $"{sql}{selectPrefix} ";
            }

            sql += $"{attributeNames} FROM {_entityType.Name}";

            sql = AppendWhereIfDefined(sql, sqlWhere);

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

            sql = AppendWhereIfDefined(sql, sqlWhere);

            return $"{sql};";
        }

        public string GenerateInsertQuery(TEntity entity, ref Dictionary<string, object> parms)
        {
            return GenerateInsertQuery(new List<TEntity> { entity }, ref parms);
        }

        public string GenerateInsertQuery(IEnumerable<TEntity> entities, ref Dictionary<string, object> parms)
        {
            SetupAttrsIfNotDefined();

            var sb = new StringBuilder();

            var entityList = entities.ToList();

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

            var sqlValueString = $"({GeneratePropertyNameString()})";

            return GenerateInsertQuery(sqlValueString, sb.ToString());
        }

        public string GenerateUpdateQuery(string sqlSet, string sqlWhere = null, object parms = null)
        {
            string sql = $"UPDATE {_entityType.Name} SET {sqlSet}";

            return $"{AppendWhereIfDefined(sql, sqlWhere)};";
        }

        #endregion

        #region Helper Methods

        private string GeneratePropertyNameString()
        {
            SetupAttrsIfNotDefined();

            // use this as your property string to prevent the table scan

            // Is this faster than using a stringbuilder?
            return _propertyNames.Select(x => $"[{x}]").Aggregate((x, y) => x + $", {y}");
        }

        private void SetupAttrsIfNotDefined()
        {
            if (_propertyNames == null)
            {
                _propertyNames = _propertyGenerator.
                    GetSqlPropertyNames<TEntity>().
                    ToList();
            }
        }

        private string AppendWhereIfDefined(string currentSql, string sqlWhere)
        {
            if (string.IsNullOrEmpty(sqlWhere))
            {
                return currentSql;
            }
            else
            {
                return $"{currentSql} WHERE {sqlWhere}";
            }
        }

        private string WrapInParenthesesIfNotWrapped(string sqlPiece)
        {
            if (sqlPiece[0] != '(')
            {
                sqlPiece = $"({sqlPiece}";
            }

            if (sqlPiece[sqlPiece.Length -1] != ')')
            {
                sqlPiece = $"{sqlPiece})";
            }

            return sqlPiece;
        }

        private Dictionary<string, object> GetParamsDict(object parms)
        {
            return parms.GetType().GetProperties()
                .ToDictionary(x => x.Name, x=> x.GetValue(parms));
        }

        #endregion
    }

    
}