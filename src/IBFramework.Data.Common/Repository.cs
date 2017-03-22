using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using System.Collections.Generic;
using Dapper;
using IBFramework.Core.Data.Domain;
using System.Linq;
using System;

namespace IBFramework.Data.Common
{
    /*
     * NO SQL should EVER be written in your code unless it is in a class
     * that specifically inherits from this repository. 
     */

    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntityWithReferences
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

        #endregion

        #region Helper Methods

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

        protected IEnumerable<T> FindBy(string joinClause = null, string whereClause = null, int? limit = null,
            int? offset = null, Dictionary<string, object> parms = null, ITranConn tc = null)
        {
            var query = _sqlGenerator.GenerateGetQuery(whereClause, joinClause, limit, offset);

            return InternalExecuteQuery(query, tc, parms);
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

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);

                            // Either Id column or an Id reference
                            if (colName.Substring(colName.Length - 2, 2) == "Id")
                            {
                                if (colName.Length > 2)
                                {
                                    fkIndices.Add(i);
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

}
