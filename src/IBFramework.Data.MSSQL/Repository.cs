using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;

namespace IBFramework.Data.MSSQL
{
    /*
     * NO SQL should EVER be written in your code unless it is in a class
     * that specifically inherits from this repository. 
     */

    public abstract class BaseRepository<T> : IBaseRepository<T>
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
        }

        public IEnumerable<T> GetAll()
        {
            var query = _sqlGenerator.GenerateGetQuery();

            return InternalExecuteQuery<T>(query);
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

        protected Task<IEnumerable<TReturn>> InternalExecuteQueryAsync<TReturn>(string sql, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction(
                async tran => await tran.Connection.QueryAsync<TReturn>(sql, parms, tran.Transaction), tc);
        }

        protected Task InternalExecuteNonQueryAsync(string sql, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction(
                async tran => await tran.Connection.ExecuteAsync(sql, parms, tran.Transaction), tc);
        }

        #endregion
    }


    public class Repository<T> : BaseRepository<T>, IRepository<T>
    {
        #region Constructor

        public Repository(
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

    //public class Repository<T, TKey> : Repository<T>, IRepository<T, TKey>
    //    where T : IEntityWithTypedId<TKey>
    //{
    //    #region Constructor

    //    public Repository(IDatabaseKeyManager databaseKeyManager)
    //        : base(databaseKeyManager)
    //    {
    //    }

    //    #endregion

    //    public void Delete(T entity, ITranConn tc = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteById(TKey id, ITranConn tc = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public T GetById(TKey id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public T SaveOrUpdate(T entity, ITranConn tc = null)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
