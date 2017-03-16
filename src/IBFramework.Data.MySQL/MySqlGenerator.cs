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
            var attributeNames = GeneratePropertyNameString(true);

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

        public string GenerateInsertQuery(TEntity entity, ref Dictionary<string, object> parms)
        {
            return GenerateInsertQuery(new List<TEntity> { entity }, ref parms);
        }

        public string GenerateUpdateQuery(TEntity entity, ref Dictionary<string, object> parms, string sqlWhere = null)
        {
            SetupAttrsIfNotDefined();

            var sb = new StringBuilder();

            //sb.Append($"UPDATE {_entityType.Name} SET ");

            for (var x = 0; x < _propertyNames.Count; x++)
            {
                var currentPropName = _propertyNames[x];
                var currentParamKey = $"@{currentPropName}";

                // Don't want to try to update the Id values, leads to failure
                // Eventually, we should use these values to update FK references
                if (currentPropName == "Id") continue;

                sb.Append($"`{currentPropName}` = {currentParamKey}");

                if (x != _propertyNames.Count - 1)
                {
                    sb.Append(", ");
                }

                GenerateEntityAttributeParams(entity, currentPropName, currentParamKey, ref parms);
            }

            return GenerateUpdateQuery(sb.ToString(), sqlWhere);
        }

        public string GenerateInsertQuery(IEnumerable<TEntity> entities, ref Dictionary<string, object> parms)
        {
            return BaseGenerateInsertReplaceQuery(entities, ref parms, true);

            //SetupAttrsIfNotDefined();

            //var sb = new StringBuilder();

            //var entityList = entities.ToList();

            //for (var y = 0; y < entityList.Count; y++)
            //{
            //    for (var x = 0; x < _propertyNames.Count; x++)
            //    {
            //        var currentPropName = _propertyNames[x];
            //        var currentParamKey = $"{currentPropName}{y}";

            //        // Don't want to try to update the Id values, leads to failure
            //        // Eventually, we should use these values to update FK references
            //        if (currentPropName == "Id") continue;

            //        // Setup the SQL Insert
            //        if (x == 0)
            //            sb.Append("(");

            //        sb.Append($"@{currentParamKey}");

            //        if (x < _propertyNames.Count - 1)
            //            sb.Append(", ");
            //        else
            //            sb.Append(")");

            //        GenerateEntityAttributeParams(entities.ElementAt(y), currentPropName, currentParamKey, ref parms);
            //    }

            //    if (y < entityList.Count - 1)
            //    {
            //        sb.Append(", ");
            //    }
            //}

            //var sqlValueString = $"({GeneratePropertyNameString(false)})";

            //return GenerateInsertQuery(sqlValueString, sb.ToString());
        }
        
        #endregion

        #region Abstract Methods

        protected override string FormatPropertyName(string propertyName)
        {
            return $"`{propertyName}`";
        }

        protected void GenerateEntityAttributeParams(TEntity entity, string currentPropName, string currentParamKey, ref Dictionary<string, object> parms)
        {
            // Setup the parameters
            object targetPropValue;

            if (currentPropName.Substring(currentPropName.Length - 2) == "Id" && currentPropName != "Id")
            {
                var targetPropertyName = currentPropName.Substring(0, currentPropName.Length - 2);
                var targetProp = _entityType.GetProperty(targetPropertyName);
                object targetPropEntity = targetProp.GetValue(entity);
                var targetPropCast = (IEntityWithTypedId<int>)targetPropEntity;

                if (targetPropCast.Id == 0)
                {
                    targetPropValue = null;
                }
                else
                {
                    targetPropValue = targetPropCast.Id;
                }
            }
            else
            {
                var targetProp = _entityType.GetProperty(currentPropName);
                targetPropValue = targetProp.GetValue(entity);
            }

            if (parms.ContainsKey(currentParamKey))
            {
                throw new Exception($"Key already in property dictionary! Key: {currentParamKey}");
            }

            parms.Add(currentParamKey, targetPropValue);
        }

        protected string BaseGenerateInsertReplaceQuery(IEnumerable<TEntity> entities, ref Dictionary<string, object> parms, bool isInsert)
        {
            SetupAttrsIfNotDefined();

            var sb = new StringBuilder();

            var entityList = entities.ToList();

            for (var y = 0; y < entityList.Count; y++)
            {
                for (var x = 0; x < _propertyNames.Count; x++)
                {
                    var currentPropName = _propertyNames[x];
                    var currentParamKey = $"{currentPropName}{y}";

                    // Don't want to try to update the Id values, leads to failure
                    // Eventually, we should use these values to update FK references
                    if (currentPropName == "Id" && isInsert) continue;

                    // Setup the SQL Insert
                    if (x == 0)
                        sb.Append("(");

                    sb.Append($"@{currentParamKey}");

                    if (x < _propertyNames.Count - 1)
                        sb.Append(", ");
                    else
                        sb.Append(")");

                    GenerateEntityAttributeParams(entities.ElementAt(y), currentPropName, currentParamKey, ref parms);
                }

                if (y < entityList.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            var sqlValueString = $"({GeneratePropertyNameString(!isInsert)})";

            var query = GenerateInsertQuery(sqlValueString, sb.ToString());

            if (isInsert)
                return query;
            else
                return query.Replace("INSERT", "REPLACE");
        }

        #endregion
    }


    public class MySqlGenerator<TEntity, TKey> : MySqlGenerator<TEntity>, ISqlGenerator<TEntity, TKey>
        where TEntity : IEntityWithTypedId<TKey>
    {
        #region Variables & Constants

        private const string idParamKey = "@entityId";
        private readonly string whereIdEqualsParam = $"`Id` = " + idParamKey;

        #endregion

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

            //parms = new Dictionary<string, object>();
            //parms.Add("@entityId", idToDelete.ToString());
            AddIdToParmsDict(idToDelete, ref parms);

            return base.GenerateDeleteQuery(whereIdEqualsParam);
        }

        public string GenerateGetQuery(TKey idToGet, ref Dictionary<string, object> parms)
        {
            if (idToGet == null) throw new Exception("Unable to get a key that is null!");

            //parms = new Dictionary<string, object>();
            //parms.Add("@entityId", idToGet.ToString());
            AddIdToParmsDict(idToGet, ref parms);

            return base.GenerateGetQuery(null, whereIdEqualsParam);
        }

        public string GenerateSaveOrUpdateQuery(TEntity entity, ref Dictionary<string, object> parms)
        {
            // Determine if we're dealing with update or delete
            bool isUpdate = false;

            if (typeof(TKey) == typeof(int))
            {
                var idInt = entity.Id as int?;

                isUpdate = idInt.HasValue && idInt.Value > 0;

                if (isUpdate)
                {
                    // Update specific query logic
                    parms.Add("@entityId", entity.Id);
                    return GenerateUpdateQuery(entity, ref parms, whereIdEqualsParam);
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
            else
            {
                return BaseGenerateInsertReplaceQuery(new List<TEntity> { entity }, ref parms, false);
            }

        }

        #endregion

        #region Helper Methods

        private void AddIdToParmsDict(TKey keyVal, ref Dictionary<string, object> parms)
        {
            var keyType = typeof(TKey);

            if (keyType == typeof(int) || keyType == typeof(string))
            {
                parms.Add(idParamKey, keyVal);
            }
            else
            {
                parms.Add(idParamKey, keyVal.ToString());
            }
        }

        #endregion
    }
}
