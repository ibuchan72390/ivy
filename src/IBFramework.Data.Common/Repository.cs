using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using System.Collections.Generic;
using Dapper;
using IBFramework.Core.Data.Domain;
using System.Linq;

namespace IBFramework.Data.Common
{
    /*
     * NO SQL should EVER be written in your code unless it is in a class
     * that specifically inherits from this repository. 
     */

    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        #region Variables & Constants

        protected readonly IDatabaseKeyManager _databaseKeyManager;
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

        public void DeleteAll(ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateDeleteQuery();

            InternalExecuteNonQuery(query, tc);

            //_tranHelper.WrapInTransaction(
            //    tran => tran.Connection.DeleteAll<T>(), tc);
        }

        public IEnumerable<T> GetAll(ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery();

            return InternalExecuteQuery<T>(query);

            //return _tranHelper.WrapInTransaction(
            //   tran => tran.Connection.GetAll<T>(), tc);
        }

        #endregion

        #region Helper Methods

        // There's got to be a way to extract all this transaction stuff into another method
        // I'd almost like to make it the tranConnGenerator piece
        protected IEnumerable<TReturn> InternalExecuteQuery<TReturn>(string sql, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction(
                tran => tran.Connection.Query<TReturn>(sql, parms, tran.Transaction), tc);
        }
        protected void InternalExecuteNonQuery(string sql, ITranConn tc = null, object parms = null)
        {
            _tranHelper.WrapInTransaction(
                tran => tran.Connection.Execute(sql, parms, tran.Transaction), tc);
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
    }


    public class BlobRepository<T> : BaseRepository<T>, IBlobRepository<T>
        where T: class
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

        public void Insert(T entity, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = _sqlGenerator.GenerateInsertQuery(entity, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
        }

        public void BulkInsert(IEnumerable<T> entities, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = _sqlGenerator.GenerateInsertQuery(entities, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
        }

        #endregion
    }

    public class Repository<T, TKey> : BaseRepository<T>, IRepository<T, TKey>
        where T : class, IEntityWithTypedId<TKey>
    {

        #region Variables & Constants

        // We populate it in the base with the correct one
        // This should work via inheritance patterns
        private ISqlGenerator<T, TKey> localSqlGenerator => (ISqlGenerator<T, TKey>)_sqlGenerator;

        #endregion

        #region Constructor

        public Repository(
            IDatabaseKeyManager databaseKeyManager,
            ITransactionHelper tranHelper,
            ISqlGenerator<T, TKey> sqlGenerator)
            :base(databaseKeyManager, tranHelper, sqlGenerator)
        {

        }

        #endregion

        #region Public Methods

        public void Delete(T entity, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateDeleteQuery(entity.Id, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
        }

        public void DeleteById(TKey id, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateDeleteQuery(id, ref parms);

            InternalExecuteNonQuery(query, tc, parms);
        }

        public T GetById(TKey id, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateGetQuery(id, ref parms);

            return InternalExecuteQuery<T>(query, tc, parms).SingleOrDefault();
        }

        public IEnumerable<T> GetByIdList(IEnumerable<TKey> ids, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var idInList = string.Join(",", ids);

            var query = _sqlGenerator.GenerateGetQuery(null, $"Id IN ({idInList})");

            return InternalExecuteQuery<T>(query, tc, parms);
        }

        public T SaveOrUpdate(T entity, ITranConn tc = null)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();

            var query = localSqlGenerator.GenerateSaveOrUpdateQuery(entity, ref parms);

            // results should contain our new Id value if this is an insert
            var results = InternalExecuteQuery<TKey>(query, tc, parms);

            if (results.Any())
            {
                entity.Id = results.First();
            }

            return entity;
        }

        #endregion
    }

    public class Repository<T> : Repository<T, int>, IRepository<T, int>
        where T : class, IEntity
    {
        public Repository(
            IDatabaseKeyManager databaseKeyManager, 
            ITransactionHelper tranHelper, 
            ISqlGenerator<T, int> sqlGenerator) 
            : base(databaseKeyManager, tranHelper, sqlGenerator)
        {
        }
    }

}
