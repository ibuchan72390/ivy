using System.Collections.Generic;
using Dapper;
using System.Linq;
using System;
using Ivy.Data.Common.Pagination;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.Data.Core.Interfaces.Pagination;
using System.Reflection;

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

        public string ConnectionString { get; private set; }

        #endregion

        #region Constructor

        public BaseRepository(
            IDatabaseKeyManager databaseKeyManager,
            ITransactionHelper tranHelper,
            ISqlGenerator<T> sqlGenerator)
        {
            _databaseKeyManager = databaseKeyManager;
            _tranHelper = tranHelper;
            _sqlGenerator = sqlGenerator;
        }

        #endregion

        #region Initialization

        public void InitializeByConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            _tranHelper.InitializeByConnectionString(connectionString);
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

        public virtual IEnumerable<T> GetAll(ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery();

            return InternalExecuteQuery(query);
        }

        public IPaginationResponse<T> GetAll(IPaginationRequest request, ITranConn tc = null)
        {
            return InternalSelectPaginated(pagingRequest: request, tc: tc);
        }

        #endregion

        #region Helper Methods

        protected IEnumerable<TBasic> GetBasicTypeList<TBasic>(string sql, Dictionary<string, object> parms, ITranConn tc = null)
        {
            return InternalExecuteAlternateTypeQuery<TBasic>(sql, tc, parms);
        }

        protected IEnumerable<T> InternalSelect(string selectPrefix = null, string joinClause = null, string whereClause = null,
            int? limit = null, int? offset = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery(selectPrefix, whereClause, joinClause, limit, offset);

            return InternalExecuteQuery(query, tc, parms);
        }

        /*
         */
        protected IPaginationResponse<T> InternalSelectPaginated(string selectPrefix = null, string joinClause = null, string whereClause = null,
            IPaginationRequest pagingRequest = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var offset = (pagingRequest.PageNumber - 1) * pagingRequest.PageCount;

            var response = new PaginationResponse<T>();

            // Data Get
            var dataQuery = _sqlGenerator.GenerateGetQuery(selectPrefix, whereClause, joinClause, pagingRequest.PageCount, offset);
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

        // Async functionality for future expansion
        //protected Task<IEnumerable<TReturn>> InternalExecuteQueryAsync<TReturn>(string sql, ITranConn tc = null, object parms = null)
        //{
        //    return _tranHelper.WrapInTransaction(
        //        async tran => await tran.Connection.QueryAsync<TReturn>(sql, parms, tran.Transaction), tc);
        //}

        //protected Task InternalExecuteNonQueryAsync(string sql, ITranConn tc = null, object parms = null)
        //{
        //    return _tranHelper.WrapInTransaction(
        //        async tran => await tran.Connection.ExecuteAsync(sql, parms, tran.Transaction), tc);
        //}

        #endregion

        #region Methods that should be private eventually....


        // There's got to be a way to extract all this transaction stuff into another method
        // I'd almost like to make it the tranConnGenerator piece
        protected virtual IEnumerable<T> InternalExecuteQuery(string sql, ITranConn tc = null, object parms = null)
        {
            //return _tranHelper.WrapInTransaction(
            //    tran => tran.Connection.Query<T>(sql, parms, tran.Transaction));

            return _tranHelper.WrapInTransaction(
                tran =>
                {
                    using (var reader = tran.Connection.ExecuteReader(sql, parms, tran.Transaction))
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

                }, tc);
        }

        protected virtual IEnumerable<TReturn> InternalExecuteAlternateTypeQuery<TReturn>(string sql, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction(
                tran => tran.Connection.Query<TReturn>(sql, parms, tran.Transaction));
        }

        protected virtual void InternalExecuteNonQuery(string sql, ITranConn tc = null, object parms = null)
        {
            _tranHelper.WrapInTransaction(
                tran => tran.Connection.Execute(sql, parms, tran.Transaction), tc);
        }

        #endregion
    }

    #endregion

    #region Blob

    public class BlobRepository<T> : BaseRepository<T>, IBlobRepository<T>
        where T: class, IEntityWithReferences
    {
        #region Constructor

        public BlobRepository(
            IDatabaseKeyManager databaseKeyManager, 
            ITransactionHelper tranHelper, 
            ISqlGenerator<T> sqlGenerator) 
            : base(databaseKeyManager, tranHelper, sqlGenerator)
        {
        }

        #endregion

        #region Public Methods

        public virtual void Insert(T entity, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = _sqlGenerator.GenerateInsertQuery(entity, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
        }

        public virtual void BulkInsert(IEnumerable<T> entities, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = _sqlGenerator.GenerateInsertQuery(entities, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
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
            ISqlGenerator<T, TKey> sqlGenerator)
            :base(databaseKeyManager, tranHelper, sqlGenerator)
        {

        }

        #endregion

        #region Public Methods

        public virtual void Delete(T entity, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateDeleteQuery(entity.Id, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
        }

        public virtual void DeleteById(TKey id, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateDeleteQuery(id, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
        }

        public virtual T GetById(TKey id, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateGetQuery(id, ref parms);

            return InternalExecuteQuery(query, tc, parms).SingleOrDefault();
        }

        public virtual IEnumerable<T> GetByIdList(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            if (ids == null || !ids.Any()) return new List<T>();

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var idInList = string.Join(",", ids);

            var query = _sqlGenerator.GenerateGetQuery(null, $"WHERE `Id` IN ({idInList})");

            return InternalExecuteQuery(query, tc, parms);
        }

        public virtual T SaveOrUpdate(T entity, ITranConn tc = null)
        {
            /*
             * Refs will not get populated on saves, only on gets
             */

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateSaveOrUpdateQuery(entity, ref parms);

            // results should contain our new Id value if this is an insert
            var results = InternalExecuteAlternateTypeQuery<TKey>(query, tc, parms);

            if (results.Any())
            {
                entity.Id = results.First();
            }

            return entity;
        }

        #endregion

        #region Helper Methods

        // There's got to be a way to extract all this transaction stuff into another method
        // I'd almost like to make it the tranConnGenerator piece
        protected override IEnumerable<T> InternalExecuteQuery(string sql, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction(
                tran =>
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

                }, tc);
        }

        #endregion
    }

    public class EntityRepository<T> : EntityRepository<T, int>, IEntityRepository<T>
        where T : class, IEntity
    {
        public EntityRepository(
            IDatabaseKeyManager databaseKeyManager, 
            ITransactionHelper tranHelper, 
            ISqlGenerator<T, int> sqlGenerator) 
            : base(databaseKeyManager, tranHelper, sqlGenerator)
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
        #region Constructor

        public EnumEntityRepository(
            IDatabaseKeyManager databaseKeyManager,
            ITransactionHelper tranHelper,
            ISqlGenerator<T, int> sqlGenerator)
            : base(databaseKeyManager, tranHelper, sqlGenerator)
        {
        }

        #endregion

        #region

        public T GetByName(TEnum enumVal, ITranConn tc = null)
        {
            const string sqlWhere = "WHERE `THIS`.`Name` = @enumVal";

            var parms = new Dictionary<string, object> { { "@enumVal", enumVal.ToString() } };

            IEnumerable<T> results = InternalSelect(null, null, sqlWhere, null, null, parms, tc);

            if (results.Count() > 1)
            {
                throw new Exception("Multiple objects found matching the provided Enumeration Value. " +
                    $"Table: {typeof(T).Name} / Search Value: {enumVal.ToString()}");
            }

            return results.FirstOrDefault();
        }

        public IEnumerable<T> GetByNames(IEnumerable<TEnum> enumVals, ITranConn tc = null)
        {
            if (enumVals == null || !enumVals.Any())
            {
                return new List<T>();
            }

            string idInList = enumVals.Select(x => $"'{x.ToString()}'").
                Aggregate((total, current) => $"{total}, {current}");

            string sqlWhere = $"WHERE `THIS`.`Name` IN ({idInList})";

            return InternalSelect(whereClause: sqlWhere, tc: tc);
        }

        #endregion
    }

    #endregion
}
