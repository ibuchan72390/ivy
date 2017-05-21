﻿using IBFramework.Data.Core.Interfaces.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IBFramework.Data.Common.Sql
{
    public abstract class BaseSqlGenerator<TEntity>
    {
        #region Variables & Constants

        protected readonly Type _entityType;
        protected readonly ISqlPropertyGenerator _propertyGenerator;

        // Cache values
        private IList<string> _propertyNames = null;
        private IList<string> _propertyNamesSansId = null;

        protected const string SelectAlias = "`THIS`";

        #endregion

        #region Constructor

        protected BaseSqlGenerator(ISqlPropertyGenerator propertyGenerator)
        {
            _entityType = typeof(TEntity);
            _propertyGenerator = propertyGenerator;
        }

        #endregion

        #region Helper Methods

        protected string GeneratePropertyNameString(bool includeId, bool includeSelectAlias)
        {
            if (!includeId)
            {
                var filteredPropNames = GetPropertyNames(false);

                filteredPropNames = filteredPropNames.Select(FormatPropertyName);

                filteredPropNames = PrependSelectAliasIfNecessary(filteredPropNames, includeSelectAlias);

                return filteredPropNames.Aggregate((x, y) => x + $", {y}");
            }
            else
            {
                // use this as your property string to prevent the table scan
                // Is this faster than using a stringbuilder?

                var adjustedPropNames = GetPropertyNames(true);

                adjustedPropNames = adjustedPropNames.Select(FormatPropertyName);

                adjustedPropNames = PrependSelectAliasIfNecessary(adjustedPropNames, includeSelectAlias);

                return adjustedPropNames.Aggregate((x, y) => x + $", {y}");
            }
        }

        //private string FormatPropertyNameCollection(IEnumerable<string> propNames)
        //{
        //    return propNames.Select(FormatPropertyName).Aggregate((x, y) => x + $", {y}");
        //}

        private IEnumerable<string> PrependSelectAliasIfNecessary(IEnumerable<string> propNames, bool includeSelectAlias)
        {
            return includeSelectAlias ?
                propNames.Select(x => $"{SelectAlias}.{x}") :
                propNames;
        }

        //protected void SetupAttrsIfNotDefined()
        //{
        //    if (_propertyNames == null)
        //    {
        //        _propertyNames = _propertyGenerator.
        //            GetSqlPropertyNames<TEntity>().
        //            ToList();
        //    }
        //}

        //protected string AppendWhereIfDefined(string currentSql, string sqlWhere)
        //{
        //    if (string.IsNullOrEmpty(sqlWhere))
        //    {
        //        return currentSql;
        //    }
        //    else
        //    {
        //        return $"{currentSql} WHERE {sqlWhere}";
        //    }
        //}

        //protected string AppendJoinIfDefined(string currentSql, string joinClause)
        //{
        //    if (string.IsNullOrEmpty(joinClause))
        //    {
        //        return currentSql;
        //    }
        //    else
        //    {
        //        return $"{currentSql} WHERE {joinClause}";
        //    }
        //}

        protected string AppendIfDefined(string currentSql, string additionalClause)
        {
            if (string.IsNullOrEmpty(additionalClause))
            {
                return currentSql;
            }
            else
            {
                return $"{currentSql} {additionalClause}";
            }
        }

        protected string WrapInParenthesesIfNotWrapped(string sqlPiece)
        {
            if (sqlPiece[0] != '(')
            {
                sqlPiece = $"({sqlPiece}";
            }

            if (sqlPiece[sqlPiece.Length - 1] != ')')
            {
                sqlPiece = $"{sqlPiece})";
            }

            return sqlPiece;
        }

        protected Dictionary<string, object> GetParamsDict(object parms)
        {
            return parms.GetType().GetProperties()
                .ToDictionary(x => x.Name, x => x.GetValue(parms));
        }

        protected IEnumerable<string> GetPropertyNames(bool includeId)
        {
            if (_propertyNames == null)
            {
                _propertyNames = _propertyGenerator.
                    GetSqlPropertyNames<TEntity>().
                    ToList();
            }

            if (includeId)
            {
                return _propertyNames;
            }
            else
            {
                if (_propertyNamesSansId == null)
                {
                    _propertyNamesSansId = _propertyNames
                        .Where(x => x != "Id")
                        .ToList();
                }

                return _propertyNamesSansId;
            }
        }

        #endregion

        #region Abstract Methods

        protected abstract string FormatPropertyName(string propertyName);

        #endregion
    }
}
