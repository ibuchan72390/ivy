using IBFramework.Core.Data.SQL;
using System;

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
     * 2) ...
     */

    /*
     * Enhancing Performance
     * 1) Don't use SELECT * to ensure that we prevent the initial table scan for properties
     */

    public class MsSqlGenerator<TEntity> : ISqlGenerator<TEntity>
    {
        private readonly Type _entityType;

        public MsSqlGenerator()
        {
            _entityType = typeof(TEntity);
        }

        public string GenerateDeleteAllQuery()
        {
            return $"DELETE FROM {_entityType.Name};";
        }

        public string GenerateGetAllQuery()
        {
            /*
             * need a way to get the enumerated values here
             */

            var attributeNames = GetObjectAttributeNames();

            return $"GET {attributeNames} FROM {_entityType.Name}";
        }

        protected string GetObjectAttributeNames()
        {
            throw new NotImplementedException();
        }
    }
}
