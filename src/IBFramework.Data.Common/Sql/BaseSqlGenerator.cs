using IBFramework.Core.Data.SQL;
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

        protected IList<string> _propertyNames = null;

        #endregion

        #region Constructor

        protected BaseSqlGenerator(ISqlPropertyGenerator propertyGenerator)
        {
            _entityType = typeof(TEntity);
            _propertyGenerator = propertyGenerator;
        }

        #endregion

        #region Helper Methods

        protected string GeneratePropertyNameString()
        {
            SetupAttrsIfNotDefined();

            // use this as your property string to prevent the table scan

            // Is this faster than using a stringbuilder?
            //return _propertyNames.Select(x => $"[{x}]").Aggregate((x, y) => x + $", {y}");
            return _propertyNames.Select(FormatPropertyName).Aggregate((x, y) => x + $", {y}");
        }

        protected void SetupAttrsIfNotDefined()
        {
            if (_propertyNames == null)
            {
                _propertyNames = _propertyGenerator.
                    GetSqlPropertyNames<TEntity>().
                    ToList();
            }
        }

        protected string AppendWhereIfDefined(string currentSql, string sqlWhere)
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

        #endregion

        #region Abstract Methods

        protected abstract string FormatPropertyName(string propertyName);

        #endregion
    }
}
