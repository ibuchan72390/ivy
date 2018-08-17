using System;
using System.Collections.Generic;
using Ivy.Data.Common.Sql;
using System.Text;
using System.Linq;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.Sql;
using Ivy.Data.Core.Domain;

namespace Ivy.Data.MySQL
{
    public class MySqlGenerator<TEntity> : BaseSqlGenerator<TEntity>, ISqlGenerator<TEntity>
    {
        #region Variables & Constants

        private string _tableName = null;

        #endregion

        #region Constructor

        public MySqlGenerator(ISqlPropertyGenerator propertyGenerator)
            : base(propertyGenerator)
        {
        }

        #endregion

        #region Public Methods

        public string GenerateGetCountQuery(string sqlWhere = null, string sqlJoin = null)
        {
            var sql = $"SELECT COUNT(*) FROM {GetTableName()} {SelectAlias}";

            sql = AppendJoinWhereOrderByIfNecessary(sql, sqlJoin, sqlWhere, null);

            sql += ";";

            return sql;
        }

        public string GenerateDeleteQuery(string sqlWhere = null)
        {
            // This was a naive mistake....
            // MySQL has a very hard time with recursive properties, particularly in queries
            // As MUCH as I fucking hate using OnDeleteCascade triggers, they're seemingly necessary for these deletions
            // If we use this instead of an OnDeleteCascade trigger, we're still going to run into issues with Delete and DeleteById
            // All in all, we pretty much have to use the OnDeleteCascade trigger until we're updated to the 8.0 engine
            // https://www.mysql.com/why-mysql/presentations/mysql-80-common-table-expressions/ - 8.0 specifics for recursive delete
            //if (sqlWhere == null)
            //{
            //    // Crazy issue with self-referencing tables, it seems that the standard Delete All does not work...
            //    // Delete all on self-referencing tables causes an FK error because it goes bottom up by Id
            //    return $"SET FOREIGN_KEY_CHECKS = 0; DELETE FROM {GetTableName()}; SET FOREIGN_KEY_CHECKS = 1;";
            //}

            var sql = AppendIfDefined($"DELETE FROM {GetTableName()}", sqlWhere);
            return $"{sql};";
        }

        public string GenerateGetQuery(string selectPrefix = null, string sqlWhere = null, string sqlJoin = null, 
            string sqlOrder = null, int? limit = default(int?), int? offset = default(int?))
        {
            if (offset.HasValue && !limit.HasValue) throw new Exception("Unable to use a limit without an offset");

            var attributeNames = GeneratePropertyNameString(true, true);

            var sql = "SELECT ";

            if (selectPrefix != null)
            {
                sql = $"{sql}{selectPrefix} ";
            }

            // We need to use the THIS as an alias in order to 
            sql += $"{attributeNames} FROM {GetTableName()} {SelectAlias}";

            sql = AppendJoinWhereOrderByIfNecessary(sql, sqlJoin, sqlWhere, sqlOrder);

            if (limit.HasValue)
            {
                sql = AppendIfDefined(sql, $"LIMIT {limit}");
            }

            if (limit.HasValue && offset.HasValue)
            {
                sql = AppendIfDefined(sql, $"OFFSET {offset}");
            }

            return $"{sql};";
        }

        public string GenerateInsertQuery(string sqlValue, string insertClause)
        {
            sqlValue = WrapInParenthesesIfNotWrapped(sqlValue);
            insertClause = WrapInParenthesesIfNotWrapped(insertClause);

            return $"INSERT INTO {GetTableName()} {sqlValue} VALUES {insertClause};";
        }

        public string GenerateUpdateQuery(string sqlSet, string sqlWhere = null)
        {
            var sql = $"UPDATE {GetTableName()} {sqlSet}";

            sql = AppendIfDefined(sql, sqlWhere);

            return $"{sql};";
        }

        public ISqlExecutionResult GenerateInsertQuery(TEntity entity, Dictionary<string, object> parms)
        {
            return GenerateInsertQuery(new List<TEntity> { entity }, parms);
        }

        public ISqlExecutionResult GenerateUpdateQuery(TEntity entity, Dictionary<string, object> parms, string sqlWhere = null)
        {
            var sb = new StringBuilder();

            sb.Append($"SET ");

            IList<string> propertyIterationList = GetPropertyNames(false).ToList();

            for (var x = 0; x < propertyIterationList.Count; x++)
            {
                var currentPropName = propertyIterationList[x];
                var currentParamKey = $"@{currentPropName}";

                // Don't want to try to update the Id values, leads to failure
                // Eventually, we should use these values to update FK references
                if (currentPropName == "Id") continue;

                sb.Append($"`{currentPropName}` = {currentParamKey}");

                if (x != propertyIterationList.Count - 1)
                {
                    sb.Append(", ");
                }

                GenerateEntityAttributeParams(entity, currentPropName, currentParamKey, ref parms);
            }

            var sql = GenerateUpdateQuery(sb.ToString(), sqlWhere);

            return new SqlExecutionResult(sql, parms);
        }

        public ISqlExecutionResult GenerateInsertQuery(IEnumerable<TEntity> entities, Dictionary<string, object> parms)
        {
            return BaseGenerateInsertReplaceQuery(entities, ref parms, true, false);
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

            var entityProps = _entityType.GetProperties();
            var currentProp = entityProps.FirstOrDefault(x => x.Name == currentPropName);

            bool isEntityAttr = false;

            if (currentProp == null &&
                currentPropName.Substring(currentPropName.Length - 2) == "Id" &&
                currentPropName != "Id")
            {
                var propName = currentPropName.Substring(0, currentPropName.Length - 2);
                currentProp = _entityType.GetProperties().FirstOrDefault(x => x.Name == propName);

                var interfaces = currentProp.PropertyType.GetInterfaces();

                var genericInterfaces = interfaces.Where(x => x.IsConstructedGenericType).Select(x => x.GetGenericTypeDefinition());

                isEntityAttr = genericInterfaces.Contains(typeof(IEntityWithTypedId<>));
            }


            if (isEntityAttr)
            {
                var targetPropertyName = currentPropName.Substring(0, currentPropName.Length - 2);
                var targetProp = _entityType.GetProperty(targetPropertyName);
                object targetPropEntity = targetProp.GetValue(entity);

                /*
                 * At this point, if the value is null, the property simply has not been populated
                 */
                if (targetPropEntity == null)
                {
                    targetPropValue = null;
                }
                else
                {
                    /*
                     * This is going to be a bad strategy, we're going to need to find another way to handle this
                     * Cast to null is fucking stupid
                     */
                    var targetPropIntCast = targetPropEntity as IEntityWithTypedId<int>;
                    var targetPropStringCast = targetPropEntity as IEntityWithTypedId<string>;

                    if (targetPropIntCast != null)
                    {
                        if (targetPropIntCast.Id == 0)
                        {
                            targetPropValue = null;
                        }
                        else
                        {
                            targetPropValue = targetPropIntCast.Id;
                        }
                    }
                    else if (targetPropStringCast != null)
                    {
                        if (targetPropStringCast.Id == null)
                        {
                            targetPropValue = null;
                        }
                        else
                        {
                            targetPropValue = targetPropStringCast.Id;
                        }
                    }
                    else
                    {
                        throw new Exception("Unable to cast the current object to an IEntityWithTypedId");
                    }
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

        protected ISqlExecutionResult BaseGenerateInsertReplaceQuery(IEnumerable<TEntity> entities, ref Dictionary<string, object> parms, bool isInsert, bool includeSelectAlias)
        {
            var sb = new StringBuilder();

            //int commaLimit = isInsert ? 2 : 1;

            //IList<string> propertyIterationList = isInsert ?
            //    _propertyNames.Where(x => x != "Id").ToList() :
            //    _propertyNames;

            IList<string> propertyIterationList = GetPropertyNames(includeId: !isInsert).ToList();


            var entityList = entities.ToList();

            for (var y = 0; y < entityList.Count; y++)
            {
                for (var x = 0; x < propertyIterationList.Count; x++)
                {
                    var currentPropName = propertyIterationList[x];
                    var currentParamKey = $"{currentPropName}{y}";

                    // Don't want to try to update the Id values, leads to failure
                    // Eventually, we should use these values to update FK references
                    if (currentPropName == "Id" && isInsert) continue;

                    // Setup the SQL Insert
                    if (x == 0)
                        sb.Append("(");

                    sb.Append($"@{currentParamKey}");

                    if (x < propertyIterationList.Count - 1)
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

            var sqlValueString = $"({GeneratePropertyNameString(!isInsert, includeSelectAlias)})";

            var query = GenerateInsertQuery(sqlValueString, sb.ToString());

            if (!isInsert)
            { 
                query = query.Replace("INSERT", "REPLACE");
            }

            return new SqlExecutionResult(query, parms);
        }

        protected string GetTableName()
        {
            /*
             * MySQL defaults to use lower case table names because of the case sensitivity of different file systems
             * - Microsoft Windows doesn't care about file case
             * - Unix systems (Lambda & RDS) do care about file case
             * - We're going to treat everything lower case on the generator, that way Entity names are nice and readable
             * 
             * http://stackoverflow.com/questions/28540573/lower-case-table-names-set-to-2-workbench-still-does-not-allow-lowercase-databa
             */

            if (_tableName == null)
            {
                _tableName = _entityType.Name.ToLower();
            }

            return _tableName;
        }

        #endregion

        #region Private Helper Methods

        private string AppendJoinWhereOrderByIfNecessary(string sql, string sqlJoin, string sqlWhere, string sqlOrderBy)
        {
            sql = AppendIfDefined(sql, sqlJoin);

            sql = AppendIfDefined(sql, sqlWhere);

            sql = AppendIfDefined(sql, sqlOrderBy);

            return sql;
        }

        #endregion
    }


    public class MySqlGenerator<TEntity, TKey> : MySqlGenerator<TEntity>, ISqlGenerator<TEntity, TKey>
        where TEntity : IEntityWithTypedId<TKey>
    {
        #region Variables & Constants

        private const string idParamKey = "@entityId";
        private readonly string whereIdEqualsParam = $"WHERE `Id` = " + idParamKey;

        #endregion

        #region Constructor

        public MySqlGenerator(ISqlPropertyGenerator sqlPropertyGenerator)
            :base(sqlPropertyGenerator)
        {

        }

        #endregion

        #region Public Methods

        public ISqlExecutionResult GenerateDeleteQuery(IEnumerable<TKey> idsToDelete, Dictionary<string, object> parms)
        {
            if (idsToDelete == null) throw new Exception("Unable to delete a key collection that is null!");

            var sqlWhere = GenerateWhereIdInList(idsToDelete, ref parms);

            var sql = base.GenerateDeleteQuery(sqlWhere);

            return new SqlExecutionResult(sql, parms);
        }

        public ISqlExecutionResult GenerateDeleteQuery(TKey idToDelete, Dictionary<string, object> parms)
        {
            if (idToDelete == null) throw new Exception("Unable to delete a key that is null!");

            //parms = new Dictionary<string, object>();
            //parms.Add("@entityId", idToDelete.ToString());
            AddIdToParmsDict(idToDelete, idParamKey, ref parms);

            var sql = base.GenerateDeleteQuery(whereIdEqualsParam);

            return new SqlExecutionResult(sql, parms);
        }

        public ISqlExecutionResult GenerateGetQuery(IEnumerable<TKey> idsToGet, Dictionary<string, object> parms)
        {
            if (idsToGet == null) throw new Exception("Unable to get a key collection that is null!");

            var sqlWhere = GenerateWhereIdInList(idsToGet, ref parms);

            var sql = base.GenerateGetQuery(sqlWhere: sqlWhere);

            return new SqlExecutionResult(sql, parms);
        }

        public ISqlExecutionResult GenerateGetQuery(TKey idToGet, Dictionary<string, object> parms)
        {
            if (idToGet == null) throw new Exception("Unable to get a key that is null!");

            //parms = new Dictionary<string, object>();
            //parms.Add("@entityId", idToGet.ToString());
            AddIdToParmsDict(idToGet, idParamKey, ref parms);

            var sql = base.GenerateGetQuery(null, whereIdEqualsParam);

            return new SqlExecutionResult(sql, parms);
        }

        public ISqlExecutionResult GenerateSaveOrUpdateQuery(TEntity entity, Dictionary<string, object> parms)
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
                    return GenerateUpdateQuery(entity, parms, whereIdEqualsParam);
                }
                else
                {
                    /*
                     * May need a way to generate some specific Id values here if the PK Identity functionality doesn't work as expected
                     * for any non-standard Id values. Integer I assume will be generated appropriately, but anything dealing with strings
                     * or guids may not be as effective when leveraging the PK Identity functionality.
                     */

                    // Insert specific query logic
                    var result = GenerateInsertQuery(entity, parms);

                    result.Sql += "SELECT LAST_INSERT_ID();";

                    return result;
                }
            }
            else
            {
                return BaseGenerateInsertReplaceQuery(new List<TEntity> { entity }, ref parms, false, false);
            }

        }

        #endregion

        #region Helper Methods

        private void AddIdToParmsDict(TKey keyVal, string key, ref Dictionary<string, object> parms)
        {
            var keyType = typeof(TKey);

            if (keyType == typeof(int) || keyType == typeof(string))
            {
                parms.Add(key, keyVal);
            }
            else
            {
                parms.Add(key, keyVal.ToString());
            }
        }

        private string GenerateWhereIdInList(IEnumerable<TKey> ids, ref Dictionary<string, object> parms)
        {
            var idParams = Enumerable.Range(0, ids.Count()).Select(x => $"@id{x}");

            var idInList = string.Join(",", idParams);

            for (var i = 0; i < ids.Count(); i++)
            {
                var idParam = idParams.ElementAt(i);
                var idVal = ids.ElementAt(i);

                AddIdToParmsDict(idVal, idParam, ref parms);
            }

            return $"WHERE `Id` IN ({idInList})";
        }

        #endregion
    }
}
