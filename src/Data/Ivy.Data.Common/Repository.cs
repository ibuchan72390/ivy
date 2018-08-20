using System.Collections.Generic;
using Dapper;
using System.Linq;
using System;
using Ivy.Data.Common.Pagination;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.Data.Core.Interfaces.Pagination;
using System.Threading.Tasks;
using System.Data;
using Ivy.Data.Core.Interfaces.Sql;

namespace Ivy.Data.Common
{
    /*
     * NO SQL should EVER be written in your code unless it is in a class
     * that specifically inherits from this repository. 
     */

    #region Base

    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntityWithReferences
    {
        #region Variables & Constants

        // Never accessed directly
        private readonly IDatabaseKeyManager _databaseKeyManager;

        // Currently accessed directly, but we should be using the internal protected methods
        protected readonly ITransactionHelper _tranHelper;
        protected readonly ISqlGenerator<T> _sqlGenerator;
        protected readonly ISqlExecutor _sqlExecutor;

        public string ConnectionString { get; private set; }

        #endregion

        #region Constructor

        public BaseRepository(
            IDatabaseKeyManager databaseKeyManager,
            ITransactionHelper tranHelper,
            ISqlGenerator<T> sqlGenerator,
            ISqlExecutor sqlExecutor)
        {
            _databaseKeyManager = databaseKeyManager;
            _tranHelper = tranHelper;
            _sqlGenerator = sqlGenerator;
            _sqlExecutor = sqlExecutor;
        }

        #endregion

        #region Initialization

        public void InitializeByConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            var connString = _databaseKeyManager.GetConnectionString(databaseKey);
            InitializeByConnectionString(connString);
        }

        #endregion

        #region Public Methods

        public virtual void DeleteAll(ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateDeleteQuery();

            InternalExecuteNonQuery(query, tc);
        }

        public virtual async Task DeleteAllAsync(ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateDeleteQuery();

            await InternalExecuteNonQueryAsync(query, tc);
        }

        public virtual IEnumerable<T> GetAll(ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery();

            return InternalExecuteQuery(query, tc);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery();

            return await InternalExecuteQueryAsync(query, tc);
        }

        public virtual IPaginationResponse<T> GetAll(IPaginationRequest request, ITranConn tc = null)
        {
            return InternalSelectPaginated(pagingRequest: request, tc: tc);
        }

        public virtual async Task<IPaginationResponse<T>> GetAllAsync(IPaginationRequest request, ITranConn tc = null)
        {
            return await InternalSelectPaginatedAsync(pagingRequest: request, tc: tc);
        }

        public virtual int GetCount(ITranConn tc = null)
        {
            return InternalCount(tc: tc);
        }

        public virtual async Task<int> GetCountAsync(ITranConn tc = null)
        {
            return await InternalCountAsync(tc: tc);
        }

        #endregion

        #region Helper Methods

        /*
         * Synchronous Support
         */
        protected IEnumerable<TBasic> GetBasicTypeList<TBasic>(string sql, Dictionary<string, object> parms, ITranConn tc = null)
        {
            return InternalExecuteAlternateTypeQuery<TBasic>(sql, tc, parms);
        }

        protected IEnumerable<T> InternalSelect(string selectPrefix = null, string joinClause = null, string whereClause = null,
            string orderByClause = null, int? limit = null, int? offset = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery(selectPrefix, whereClause, joinClause, orderByClause, limit, offset);

            return InternalExecuteQuery(query, tc, parms);
        }

        protected IPaginationResponse<T> InternalSelectPaginated(string selectPrefix = null, string joinClause = null, string whereClause = null,
            string orderByClause = null, IPaginationRequest pagingRequest = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var offset = (pagingRequest.PageNumber - 1) * pagingRequest.PageCount;

            var response = new PaginationResponse<T>();

            // Data Get
            var dataQuery = _sqlGenerator.GenerateGetQuery(selectPrefix, whereClause, joinClause, orderByClause, pagingRequest.PageCount, offset);
            response.Data = InternalExecuteQuery(dataQuery, tc, parms);

            // Count Get
            // need to pass where and join for proper understanding of filtered result set
            var countQuery = _sqlGenerator.GenerateGetCountQuery(whereClause, joinClause);
            response.TotalCount = InternalExecuteAlternateTypeQuery<int>(countQuery, tc, parms).FirstOrDefault();

            return response;
        }

        protected void InternalUpdate(string setClause, string whereClause = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var sql = _sqlGenerator.GenerateUpdateQuery(setClause, whereClause);

            InternalExecuteNonQuery(sql, tc, parms);
        }

        protected void InternalDelete(string whereClause = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var sql = _sqlGenerator.GenerateDeleteQuery(whereClause);

            InternalExecuteNonQuery(sql, tc, parms);
        }

        protected int InternalCount(string whereClause = null, string joinClause = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var countQuery = _sqlGenerator.GenerateGetCountQuery(whereClause, joinClause);

            return InternalExecuteAlternateTypeQuery<int>(countQuery, tc, parms).SingleOrDefault();
        }



        /*
        * Asynchronous Support
        */
        protected async Task<IEnumerable<TBasic>> GetBasicTypeListAsync<TBasic>(string sql, Dictionary<string, object> parms, ITranConn tc = null)
        {
            return await InternalExecuteAlternateTypeQueryAsync<TBasic>(sql, tc, parms);
        }

        protected async Task<IEnumerable<T>> InternalSelectAsync(string selectPrefix = null, string joinClause = null, string whereClause = null,
            string orderByClause = null, int? limit = null, int? offset = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery(selectPrefix, whereClause, joinClause, orderByClause, limit, offset);

            return await InternalExecuteQueryAsync(query, tc, parms);
        }

        protected async Task<IPaginationResponse<T>> InternalSelectPaginatedAsync(string selectPrefix = null, string joinClause = null, string whereClause = null,
            string orderByClause = null, IPaginationRequest pagingRequest = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var offset = (pagingRequest.PageNumber - 1) * pagingRequest.PageCount;

            var response = new PaginationResponse<T>();

            // Data Get
            var dataQuery = _sqlGenerator.GenerateGetQuery(selectPrefix, whereClause, joinClause, orderByClause, pagingRequest.PageCount, offset);
            response.Data = await InternalExecuteQueryAsync(dataQuery, tc, parms);

            // Count Get
            // need to pass where and join for proper understanding of filtered result set
            var countQuery = _sqlGenerator.GenerateGetCountQuery(whereClause, joinClause);
            var countResults = await InternalExecuteAlternateTypeQueryAsync<int>(countQuery, tc, parms);
            response.TotalCount = countResults.FirstOrDefault();

            return response;
        }

        protected async Task InternalUpdateAsync(string setClause, string whereClause = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var sql = _sqlGenerator.GenerateUpdateQuery(setClause, whereClause);

            await InternalExecuteNonQueryAsync(sql, tc, parms);
        }

        protected async Task InternalDeleteAsync(string whereClause = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var sql = _sqlGenerator.GenerateDeleteQuery(whereClause);

            await InternalExecuteNonQueryAsync(sql, tc, parms);
        }

        protected async Task<int> InternalCountAsync(string whereClause = null, string joinClause = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var countQuery = _sqlGenerator.GenerateGetCountQuery(whereClause, joinClause);

            var countResults = await InternalExecuteAlternateTypeQueryAsync<int>(countQuery, tc, parms);
            
            return countResults.SingleOrDefault();
        }

        #endregion

        #region Methods that should be private eventually....


        // There's got to be a way to extract all this transaction stuff into another method
        // I'd almost like to make it the tranConnGenerator piece


        /*
         * Synchronous Support
         */
        protected virtual IEnumerable<T> InternalExecuteQuery(string sql, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction<IEnumerable<T>>(
                    tran => HandleExecuteQuery(tran, sql, parms), ConnectionString, tc);
        }

        protected virtual IEnumerable<TReturn> InternalExecuteAlternateTypeQuery<TReturn>(string sql, ITranConn tc = null, object parms = null)
        {
            return _sqlExecutor.ExecuteTypedQuery<TReturn>(sql, ConnectionString, tc, parms);
        }

        protected virtual void InternalExecuteNonQuery(string sql, ITranConn tc = null, object parms = null)
        {
            _sqlExecutor.ExecuteNonQuery(sql, ConnectionString, tc, parms);
        }


        /*
         * Asynchronous Support
         */
        protected virtual async Task<IEnumerable<T>> InternalExecuteQueryAsync(string sql, ITranConn tc = null, object parms = null)
        {
            return await _tranHelper.WrapInTransactionAsync<IEnumerable<T>>(
                    async tran => await HandleExecuteQueryAsync(tran, sql, parms), ConnectionString, tc);
        }

        protected virtual async Task<IEnumerable<TReturn>> InternalExecuteAlternateTypeQueryAsync<TReturn>(string sql, ITranConn tc = null, object parms = null)
        {
            return await _sqlExecutor.ExecuteTypedQueryAsync<TReturn>(sql, ConnectionString, tc, parms);
        }

        protected virtual async Task InternalExecuteNonQueryAsync(string sql, ITranConn tc = null, object parms = null)
        {
            await _sqlExecutor.ExecuteNonQueryAsync(sql, ConnectionString, tc, parms);
        }

        #endregion

        #region Private Methods

        private IEnumerable<T> HandleExecuteQuery(ITranConn tran, string sql, object parms)
        {
            using (IDataReader reader = tran.Connection.ExecuteReader(sql, parms, tran.Transaction))
            {
                return ProcessIDataReader(reader);
            }
        }

        private async Task<IEnumerable<T>> HandleExecuteQueryAsync(ITranConn tran, string sql, object parms)
        {
            using (IDataReader reader = await tran.Connection.ExecuteReaderAsync(sql, parms, tran.Transaction))
            {
                return ProcessIDataReader(reader);
            }
        }

        private IEnumerable<T> ProcessIDataReader(IDataReader reader)
        {
            IList<int> fkIndices = new List<int>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var colName = reader.GetName(i);

                // Either Id column or an Id reference
                if (colName.Substring(colName.Length - 2, 2) == "Id" && colName.Length > 2)
                {
                    fkIndices.Add(i);
                }
            }

            var parser = reader.GetRowParser<T>();

            IList<T> results = new List<T>();

            while (reader.Read())
            {
                var referenceList = fkIndices.ToDictionary(x => reader.GetName(x), x => reader.GetValue(x));

                T result = parser(reader);

                result.References = referenceList;

                results.Add(result);
            }

            reader.Close();

            return results;
        }

        #endregion
    }

    #endregion

    #region Blob

    public class BlobRepository<T> : BaseRepository<T>, IBlobRepository<T>
        where T : class, IEntityWithReferences
    {
        #region Constructor

        public BlobRepository(
            IDatabaseKeyManager databaseKeyManager,
            ITransactionHelper tranHelper,
            ISqlGenerator<T> sqlGenerator,
            ISqlExecutor sqlExecutor)
            : base(databaseKeyManager, tranHelper, sqlGenerator, sqlExecutor)
        {
        }

        #endregion

        #region Public Methods

        public virtual void Insert(T entity, ITranConn tc = null)
        {
            var query = GenerateInsertQuery(entity);

            InternalExecuteNonQuery(query.Sql, tc, query.Parms);
        }

        public async Task InsertAsync(T entity, ITranConn tc = null)
        {
            var query = GenerateInsertQuery(entity);

            await InternalExecuteNonQueryAsync(query.Sql, tc, query.Parms);
        }

        public virtual void BulkInsert(IEnumerable<T> entities, ITranConn tc = null)
        {
            var query = GenerateBulkInsertQuery(entities);

            InternalExecuteNonQuery(query.Sql, tc, query.Parms);
        }

        public async Task BulkInsertAsync(IEnumerable<T> entities, ITranConn tc = null)
        {
            var query = GenerateBulkInsertQuery(entities);

            await InternalExecuteNonQueryAsync(query.Sql, tc, query.Parms);
        }

        #endregion

        #region Private Helper Methods

        private ISqlExecutionResult GenerateInsertQuery(T entity)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            return _sqlGenerator.GenerateInsertQuery(entity, parms);
        }

        private ISqlExecutionResult GenerateBulkInsertQuery(IEnumerable<T> entities)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            return _sqlGenerator.GenerateInsertQuery(entities, parms);
        }

        #endregion
    }

    #endregion

    #region Entity

    public class EntityRepository<T, TKey> : BaseRepository<T>, IEntityRepository<T, TKey>
        where T : class, IEntityWithTypedId<TKey>
    {

        #region Variables & Constants

        // We populate it in the base with the correct one
        // This should work via inheritance patterns
        private ISqlGenerator<T, TKey> localSqlGenerator => (ISqlGenerator<T, TKey>)_sqlGenerator;

        #endregion

        #region Constructor

        public EntityRepository(
            IDatabaseKeyManager databaseKeyManager,
            ITransactionHelper tranHelper,
            ISqlGenerator<T, TKey> sqlGenerator,
            ISqlExecutor sqlExecutor)
            :base(databaseKeyManager, tranHelper, sqlGenerator, sqlExecutor)
        {

        }

        #endregion

        #region Public Methods

        public virtual void Delete(T entity, ITranConn tc = null)
        {
            var query = localSqlGenerator.GenerateDeleteQuery(entity.Id, new Dictionary<string, object>());

            InternalExecuteNonQuery(query.Sql, tc, query.Parms);
        }

        public async Task DeleteAsync(T entity, ITranConn tc = null)
        {
            var query = localSqlGenerator.GenerateDeleteQuery(entity.Id, new Dictionary<string, object>());

            await InternalExecuteNonQueryAsync(query.Sql, tc, query.Parms);
        }

        public virtual void DeleteById(TKey id, ITranConn tc = null)
        {
            var query = localSqlGenerator.GenerateDeleteQuery(id, new Dictionary<string, object>());

            InternalExecuteNonQuery(query.Sql, tc, query.Parms);
        }

        public async Task DeleteByIdAsync(TKey id, ITranConn tc = null)
        {
            var query = localSqlGenerator.GenerateDeleteQuery(id, new Dictionary<string, object>());

            await InternalExecuteNonQueryAsync(query.Sql, tc, query.Parms);
        }

        public virtual void Delete(IEnumerable<T> entities, ITranConn tc = null)
        {
            if (entities == null || !entities.Any()) return;

            var idList = entities.Select(x => x.Id);

            DeleteByIdList(idList, tc);
        }

        public async Task DeleteAsync(IEnumerable<T> entities, ITranConn tc = null)
        {
            if (entities == null || !entities.Any()) return;

            var idList = entities.Select(x => x.Id);

            await DeleteByIdListAsync(idList, tc);
        }

        public virtual void DeleteByIdList(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            if (ids == null || !ids.Any()) return;

            var query = localSqlGenerator.GenerateDeleteQuery(ids, new Dictionary<string, object>());

            InternalExecuteNonQuery(query.Sql, tc, query.Parms);
        }

        public async Task DeleteByIdListAsync(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            if (ids == null || !ids.Any()) return;

            var query = localSqlGenerator.GenerateDeleteQuery(ids, new Dictionary<string, object>());

            await InternalExecuteNonQueryAsync(query.Sql, tc, query.Parms);
        }

        public virtual T GetById(TKey id, ITranConn tc = null)
        {
            var query = localSqlGenerator.GenerateGetQuery(id, new Dictionary<string, object>());

            return InternalExecuteQuery(query.Sql, tc, query.Parms).SingleOrDefault();
        }

        public async Task<T> GetByIdAsync(TKey id, ITranConn tc = null)
        {
            var query = localSqlGenerator.GenerateGetQuery(id, new Dictionary<string, object>());

            var results = await InternalExecuteQueryAsync(query.Sql, tc, query.Parms);
            
            return results.SingleOrDefault();
        }

        public virtual IEnumerable<T> GetByIdList(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            if (ids == null || !ids.Any()) return new List<T>();

            var query = localSqlGenerator.GenerateGetQuery(ids, new Dictionary<string, object>());

            return InternalExecuteQuery(query.Sql, tc, query.Parms);
        }

        public async Task<IEnumerable<T>> GetByIdListAsync(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            if (ids == null || !ids.Any()) return new List<T>();

            var query = localSqlGenerator.GenerateGetQuery(ids, new Dictionary<string, object>());

            return await InternalExecuteQueryAsync(query.Sql, tc, query.Parms);
        }

        public virtual T SaveOrUpdate(T entity, ITranConn tc = null)
        {
            /*
             * Refs will not get populated on saves, only on gets
             */

            var query = localSqlGenerator.GenerateSaveOrUpdateQuery(entity, new Dictionary<string, object>());

            // results should contain our new Id value if this is an insert
            var results = InternalExecuteAlternateTypeQuery<TKey>(query.Sql, tc, query.Parms);

            if (results.Any())
            {
                entity.Id = results.First();
            }

            return entity;
        }

        public async Task<T> SaveOrUpdateAsync(T entity, ITranConn tc = null)
        {
            var query = localSqlGenerator.GenerateSaveOrUpdateQuery(entity, new Dictionary<string, object>());

            // results should contain our new Id value if this is an insert
            var results = await InternalExecuteAlternateTypeQueryAsync<TKey>(query.Sql, tc, query.Parms);

            if (results.Any())
            {
                entity.Id = results.First();
            }

            return entity;
        }

        public IEnumerable<T> SaveOrUpdate(IEnumerable<T> entities, ITranConn tc = null)
        {
            _tranHelper.WrapInTransaction(tran => 
            {
                foreach (var entity in entities)
                {
                    SaveOrUpdate(entity, tran);
                }
            }, ConnectionString, tc);

            return entities;
        }

        public async Task<IEnumerable<T>> SaveOrUpdateAsync(IEnumerable<T> entities, ITranConn tc = null)
        {
            await _tranHelper.WrapInTransactionAsync(async tran =>
            {
                foreach (var entity in entities)
                {
                    await SaveOrUpdateAsync(entity, tran);
                }
            }, ConnectionString, tc);

            return entities;
        }

        #endregion

        #region Helper Methods

        // There's got to be a way to extract all this transaction stuff into another method
        // I'd almost like to make it the tranConnGenerator piece
        protected override IEnumerable<T> InternalExecuteQuery(string sql, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction(tran => InternalHandleQueryTransaction(tran, sql, parms), ConnectionString, tc);
        }

        private IEnumerable<T> InternalHandleQueryTransaction(ITranConn tran, string sql, object parms)
        {
            using (var reader = tran.Connection.ExecuteReader(sql, parms, tran.Transaction))
            {
                IList<int> fkIndices = new List<int>();
                int idIndex = -1;

                var props = typeof(T).GetProperties();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var colName = reader.GetName(i);

                    // Either Id column or an Id reference
                    // Is there some way we can check that this is an object entity reference???
                    if (colName.Substring(colName.Length - 2, 2) == "Id")
                    {
                        if (colName.Length > 2)
                        {
                            /*
                             * May want to find some way to cache these values to ensure performance
                             */

                            var targetPropName = colName.Substring(0, colName.Length - 2);

                            // Some attribute names will end in Id even though they aren't truly FK objects
                            // In order to counteract this, we need to find the prop and verify it's an FK
                            // If no prop exists when we drop the "Id" from the attr name, it's not an FK
                            var targetProp = props.FirstOrDefault(x => x.Name == targetPropName);

                            if (targetProp != null)
                            {
                                var declaringInterfaces = targetProp.DeclaringType.GetInterfaces();

                                // Need to use name based matching here in order to circumnavigate the generic context
                                // If we don't, the actual TType affects the matching
                                var entityInterface = declaringInterfaces.FirstOrDefault(x => x.Name == typeof(IEntityWithTypedId<>).Name);

                                if (entityInterface != null)
                                {
                                    fkIndices.Add(i);
                                }
                            }
                        }
                        else
                        {
                            idIndex = i;
                        }
                    }
                }

                if (idIndex == -1)
                {
                    throw new Exception("Unable to determine the Id column for reference generation!");
                }

                var parser = reader.GetRowParser<T>();

                IList<T> results = new List<T>();

                while (reader.Read())
                {
                    var referenceList = fkIndices.ToDictionary(x => reader.GetName(x), x => reader.GetValue(x));
                    //resultsRefsDict.Add((TKey)reader.GetValue(idIndex), referenceList);

                    T result = parser(reader);

                    result.References = referenceList;

                    results.Add(result);
                }

                reader.Close();

                return results;
            }
        }

        #endregion
    }

    public class EntityRepository<T> : EntityRepository<T, int>, IEntityRepository<T>
        where T : class, IEntity
    {
        public EntityRepository(
            IDatabaseKeyManager databaseKeyManager, 
            ITransactionHelper tranHelper, 
            ISqlGenerator<T, int> sqlGenerator,
            ISqlExecutor sqlExecutor) 
            : base(databaseKeyManager, tranHelper, sqlGenerator, sqlExecutor)
        {
        }
    }

    #endregion

    #region EnumEntity

    //public class EnumEntityRepository<T, TKey, TEnum> : EntityRepository<T, TKey>, IEnumEntityRepository<T, TKey, TEnum>
    //    where T : class, IEnumEntityWithTypedId<TKey, TEnum>
    //    where TEnum : struct, IComparable, IFormattable, IConvertible
    //{

    //    #region Constructor

    //    public EnumEntityRepository(
    //        IDatabaseKeyManager databaseKeyManager,
    //        ITransactionHelper tranHelper,
    //        ISqlGenerator<T, TKey> sqlGenerator)
    //        : base(databaseKeyManager, tranHelper, sqlGenerator)
    //    {

    //    }

    //    #endregion

    //    #region

    //    public T GetByName(TEnum enumVal, ITranConn tc = null)
    //    {
    //        const string sqlWhere = "WHERE `THIS`.`Name` = @enumVal";

    //        var parms = new Dictionary<string, object> { { "@enumVal", enumVal.ToString() } };

    //        var results = InternalSelect(whereClause: sqlWhere, parms: parms, tc: tc);

    //        if (results.Count() > 1)
    //        {
    //            throw new Exception("Multiple objects found matching the provided Enumeration Value. " + 
    //                $"Table: {typeof(T).Name} / Search Value: {enumVal.ToString()}");
    //        }

    //        return results.FirstOrDefault();
    //    }

    //    #endregion
    //}

    public class EnumEntityRepository<T, TEnum> : EntityRepository<T>, IEnumEntityRepository<T, TEnum>
        where T : class, IEnumEntity<TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        #region Variables & Constants

        private const string whereEnumName = "WHERE `THIS`.`Name` = @enumVal";

        #endregion

        #region Constructor

        public EnumEntityRepository(
            IDatabaseKeyManager databaseKeyManager,
            ITransactionHelper tranHelper,
            ISqlGenerator<T, int> sqlGenerator,
            ISqlExecutor sqlExecutor)
            : base(databaseKeyManager, tranHelper, sqlGenerator, sqlExecutor)
        {
        }

        #endregion

        #region

        public virtual T GetByName(TEnum enumVal, ITranConn tc = null)
        {
            IEnumerable<T> results = InternalSelect(null, null, whereEnumName, null, null, null, GetEnumNameDict(enumVal), tc);

            return ValidateGetByNameResult(enumVal, results);
        }

        public async Task<T> GetByNameAsync(TEnum enumVal, ITranConn tc = null)
        {
            IEnumerable<T> results = await InternalSelectAsync(null, null, whereEnumName, null, null, null, GetEnumNameDict(enumVal), tc);

            return ValidateGetByNameResult(enumVal, results);
        }

        public virtual IEnumerable<T> GetByNames(IEnumerable<TEnum> enumVals, ITranConn tc = null)
        {
            if (enumVals == null || !enumVals.Any())
            {
                return new List<T>();
            }

            string sqlWhere = GenerateGetByNamesWhere(enumVals);

            return InternalSelect(whereClause: sqlWhere, tc: tc);
        }

        public async Task<IEnumerable<T>> GetByNamesAsync(IEnumerable<TEnum> enumVals, ITranConn tc = null)
        {
            if (enumVals == null || !enumVals.Any())
            {
                return new List<T>();
            }

            string sqlWhere = GenerateGetByNamesWhere(enumVals);

            return await InternalSelectAsync(whereClause: sqlWhere, tc: tc);
        }

        #endregion

        #region Helper Methods

        private Dictionary<string, object> GetEnumNameDict(TEnum enumVal)
        {
            return new Dictionary<string, object> { { "@enumVal", enumVal.ToString() } };
        }

        private T ValidateGetByNameResult(TEnum enumVal, IEnumerable<T> results)
        {
            if (results.Count() > 1)
            {
                throw new Exception("Multiple objects found matching the provided Enumeration Value. " +
                    $"Table: {typeof(T).Name} / Search Value: {enumVal.ToString()}");
            }

            return results.FirstOrDefault();
        }

        private string GenerateGetByNamesWhere(IEnumerable<TEnum> enumVals)
        {
            string idInList = enumVals.Select(x => $"'{x.ToString()}'").
                Aggregate((total, current) => $"{total}, {current}");

            return $"WHERE `THIS`.`Name` IN ({idInList})";
        }

        #endregion
    }

    #endregion
}
