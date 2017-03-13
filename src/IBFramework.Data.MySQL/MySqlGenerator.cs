using System;
using System.Collections.Generic;
using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.SQL;
using IBFramework.Data.Common.Sql;
using System.Text;
using System.Linq;
using System.Reflection;

namespace IBFramework.Data.MySQL
{
    public class MySqlGenerator<TEntity> : BaseSqlGenerator<TEntity>, ISqlGenerator<TEntity>
    {
        #region Constructor

        public MySqlGenerator(ISqlPropertyGenerator propertyGenerator)
            : base(propertyGenerator)
        {
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
            var sql = $"UPDATE {_entityType.Name} SET {sqlSet}";

            sql = AppendWhereIfDefined(sql, sqlWhere);

            return $"{sql};";
        }

        public string GenerateUpdateQuery(TEntity entity, ref Dictionary<string, object> parms, string sqlWhere = null)
        {
            SetupAttrsIfNotDefined();

            var sb = new StringBuilder();

            sb.Append($"UPDATE {_entityType.Name} SET ");

            for (var x = 0; x < _propertyNames.Count; x++)
            {
                var currentPropName = _propertyNames[x];

                // Don't want to try to update the Id values, leads to failure
                if (currentPropName == "Id") continue;

                sb.Append($"`{currentPropName}` = @{currentPropName}");

                // Setup the parameters
                var targetProp = _entityType.GetProperty(currentPropName);

                if (parms.ContainsKey(currentPropName))
                {
                    throw new Exception($"Key already in property dictionary! Key: {currentPropName}");
                }

                parms.Add(currentPropName, targetProp.GetValue(entity));

                if (x != _propertyNames.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            var sqlValueString = $"({GeneratePropertyNameString()})";

            return sb.ToString();
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

        #region Abstract Methods

        protected override string FormatPropertyName(string propertyName)
        {
            return $"`{propertyName}`";
        }

        #endregion
    }


    public class MySqlGenerator<TEntity, TKey> : MySqlGenerator<TEntity>, ISqlGenerator<TEntity, TKey>
        where TEntity : IEntityWithTypedId<TKey>
    {
        #region Constructor

        public MySqlGenerator(ISqlPropertyGenerator sqlPropertyGenerator)
            :base(sqlPropertyGenerator)
        {

        }

        #endregion

        #region Public Methods

        public string GenerateDeleteQuery(TKey idToDelete, ref Dictionary<string, object> parms)
        {
            if (idToDelete == null) throw new Exception("Unable to delete a key that is null!");

            parms = new Dictionary<string, object>();
            parms.Add("@DeleteId", idToDelete);

            return base.GenerateDeleteQuery("`Id` = @DeleteId");
        }

        public string GenerateGetQuery(TKey idToGet, ref Dictionary<string, object> parms)
        {
            if (idToGet == null) throw new Exception("Unable to get a key that is null!");

            parms = new Dictionary<string, object>();
            parms.Add("@DeleteId", idToGet);

            return base.GenerateGetQuery(null, "`Id` = @DeleteId");
        }

        public string GenerateSaveOrUpdateQuery(TEntity entity, ref Dictionary<string, object> parms)
        {
            // Determine if we're dealing with update or delete
            bool isUpdate = false;

            if (typeof(TKey) == typeof(int))
            {
                var idInt = entity.Id as int?;

                isUpdate = idInt.HasValue && idInt.Value > 0;
            }
            else
            {
                isUpdate = entity.Id != null;
            }

            if (isUpdate)
            {
                // Update specific query logic
                parms.Add("@entityId", entity.Id);
                return GenerateUpdateQuery(entity, ref parms, " WHERE Id = @entityId ");
            }
            else
            {
                /*
                 * May need a way to generate some specific Id values here if the PK Identity functionality doesn't work as expected
                 * for any non-standard Id values. Integer I assume will be generated appropriately, but anything dealing with strings
                 * or guids may not be as effective when leveraging the PK Identity functionality.
                 */

                // Insert specific query logic
                return GenerateInsertQuery(entity, ref parms) + "SELECT LAST_INSERT_ID();";
            }
        }

        #endregion

    }
}
