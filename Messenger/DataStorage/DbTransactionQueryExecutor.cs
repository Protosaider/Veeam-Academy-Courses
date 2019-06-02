using DataStorage.Mappers;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using DataStorage.Other;

namespace DataStorage
{
    public sealed class CDbTransactionQueryExecutor : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly TransactionScope _transaction;
        private static readonly ILog s_log = SLogger.GetLogger();
        //private Boolean _isDisposed;


        //public DbTransactionQueryExecutor(CDataStorageSettings settings)
        //{
        //    if (settings == null)
        //        throw new NullReferenceException(nameof(settings));

        //    try
        //    {
        //        _connection = new SqlConnection(settings.DbConnectionString);
        //        _transaction = new TransactionScope();
        //    }
        //    catch (Exception ex)
        //    {
        //        Dispose();
        //        s_log.LogError($"{nameof(DbTransactionQueryExecutor)}({settings})", ex);
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}

        public static CDbTransactionQueryExecutor Create(CDataStorageSettings settings)
        {
            if (settings == null)
                throw new NullReferenceException(nameof(settings));

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(settings.DbConnectionString))
                {
                    return new CDbTransactionQueryExecutor(dbConnection, TransactionScopeOption.Required, System.Transactions.IsolationLevel.Serializable, TransactionManager.DefaultTimeout);
                }
            }
            catch (Exception ex)
            {
                s_log.LogError($"{nameof(Create)}({settings})", ex);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

		internal CDbTransactionQueryExecutor(IDbConnection connection)
        {
            if (connection == null)
                throw new NullReferenceException(nameof(connection));

            try
            {
                _connection = connection;
                TransactionOptions transactionOptions = new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.Serializable,
                    Timeout = TransactionManager.DefaultTimeout
                };
                _transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
            }
            catch (Exception ex)
            {
                Dispose();
                s_log.LogError($"{nameof(CDbTransactionQueryExecutor)}({connection})", ex);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private CDbTransactionQueryExecutor(IDbConnection connection, TransactionScopeOption transactionScope, System.Transactions.IsolationLevel isolationLevel, TimeSpan transactionTimeout)
        {
            if (connection == null)
                throw new NullReferenceException(nameof(connection));

            try
            {
                _connection = connection;
                TransactionOptions transactionOptions = new TransactionOptions()
                {
                    IsolationLevel = isolationLevel,
                    Timeout = transactionTimeout
                };
                _transaction = new TransactionScope(transactionScope, transactionOptions);
            }
            catch (Exception ex)
            {
                Dispose();
                s_log.LogError($"{nameof(CDbTransactionQueryExecutor)}({connection})", ex);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #region MSSQL

		internal T ExecuteScalar<T>(String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return ExecuteScalar<T>(command, queryString, parameters);
            }
        }

		internal List<T> GetData<T>(IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return GetData<T>(command, mapper, queryString, parameters);
            }
        }

		internal T GetItem<T>(IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return GetItem<T>(command, mapper, queryString, parameters);
            }
        }

		private Int32 ExecuteNonQuery(String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return ExecuteNonQuery(command, queryString, parameters);
            }
        }

        #endregion

		internal Int32 CreateItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);
		internal Int32 DeleteItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);
        public Int32 InsertItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);
		internal Int32 UpdateItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);


		private T ExecuteScalar<T>(IDbCommand command, String queryString, params SqlParameter[] parameters)
        {
            T result = default(T);

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                Object resultObj = command.ExecuteScalar();
                if (resultObj != DBNull.Value)
                    result = (T)resultObj;
            }
            catch (Exception ex)
            {
                s_log.LogError($"{nameof(ExecuteScalar)}({queryString}, {parameters})", ex);
                Console.WriteLine(ex.Message);
                throw;
            }

            return result;
        }


		private List<T> GetData<T>(IDbCommand command, IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            var result = new List<T>();

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(mapper.ReadItem(reader));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                s_log.LogError($"{nameof(GetData)}({mapper}, {queryString}, {parameters})", ex);
                Console.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

		private T GetItem<T>(IDbCommand command, IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            T result = default(T);

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    result = mapper.ReadItem(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                s_log.LogError($"{nameof(GetItem)}({mapper}, {queryString}, {parameters})", ex);
                Console.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

		private Int32 ExecuteNonQuery(IDbCommand command, String queryString, params SqlParameter[] parameters)
        {
            Int32 rowsAffected;

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                s_log.LogError($"{nameof(ExecuteNonQuery)}({queryString}, {parameters})", ex);
                Console.WriteLine(ex.Message);
                throw;
            }

            return rowsAffected;
        }

		internal void Commit()
        {
            _transaction?.Complete();
            s_log.LogInfo(@"Transaction is committed");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            //if (_isDisposed)
            //    return;

            if (disposing)
            {
                _connection?.Close();

                _transaction?.Dispose();
                _connection?.Dispose();

                //_transaction = null;
                //_connection = null;
            }
            //_isDisposed = true;
        }

        ~CDbTransactionQueryExecutor()
        {
            Dispose(false);
        }

    }
}
