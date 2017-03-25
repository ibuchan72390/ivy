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

        public string GenerateDeleteQuery(string sqlWhere = null)
        {
            var sql = AppendIfDefined($"DELETE FROM {GetTableName()}", sqlWhere);
            return $"{sql};";
        }

        public string GenerateGetQuery(string sqlWhere = null, string sqlJoin = null, int? limit = default(int?), int? offset = default(int?))
        {
            if (offset.HasValue && !limit.HasValue) throw new Exception("Unable to use a limit without an offset");

            var attributeNames = GeneratePropertyNameString(true, true);

            var sql = "SELECT ";

            // We need to use the THIS as an alias in order to 
            sql += $"{attributeNames} FROM {GetTableName()} {SelectAlias}";

            sql = AppendIfDefined(sql, sqlJoin);

            sql = AppendIfDefined(sql, sqlWhere);

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

        public string GenerateInsertQuery(TEntity entity, ref Dictionary<string, object> parms)
        {
            return GenerateInsertQuery(new List<TEntity> { entity }, ref parms);
        }

        public string GenerateUpdateQuery(TEntity entity, ref Dictionary<string, object> parms, string sqlWhere = null)
        {
            SetupAttrsIfNotDefined();

            var sb = new StringBuilder();

            sb.Append($"SET ");

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

            var currentProp = _entityType.GetProperties().FirstOrDefault(x => x.Name == currentPropName);

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

        protected string BaseGenerateInsertReplaceQuery(IEnumerable<TEntity> entities, ref Dictionary<string, object> parms, bool isInsert, bool includeSelectAlias)
        {
            SetupAttrsIfNotDefined();

            var sb = new StringBuilder();

            //int commaLimit = isInsert ? 2 : 1;

            IList<string> propertyIterationList = isInsert ?
                _propertyNames.Where(x => x != "Id").ToList() :
                _propertyNames;

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

            if (isInsert)
                return query;
            else
                return query.Replace("INSERT", "REPLACE");
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
                return BaseGenerateInsertReplaceQuery(new List<TEntity> { entity }, ref parms, false, false);
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
