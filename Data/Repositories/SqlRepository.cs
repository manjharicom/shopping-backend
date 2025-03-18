using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class SqlRepository : IDisposable
    {
        private const int CommandTimeout = 300;
        private readonly string _connectionString;
        private readonly IDbConnection _connection;

        protected SqlRepository(IDbConnection connection)
        {
            _connectionString = connection.ConnectionString;
            _connection = connection;
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string procName, object parameters = null)
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout).ConfigureAwait(continueOnCapturedContext: false);
        }

        protected async Task<T> QuerySingleAsync<T>(string procName, object parameters = null)
        {
            using var connection = GetConnection();
            return await connection.QuerySingleOrDefaultAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout).ConfigureAwait(continueOnCapturedContext: false);
        }

        protected async Task<int> ExecuteAsync(string procName, object parameters = null)
        {
            using var connection = GetConnection();
            return await connection.ExecuteAsync(procName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout).ConfigureAwait(continueOnCapturedContext: false);
        }

        protected async Task<T> ExecuteSingleAsync<T>(string procName, object parameters = null)
        {
            using (var connection = GetConnection())
            return await connection.ExecuteScalarAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout).ConfigureAwait(continueOnCapturedContext: false);
        }

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);

            return connection;
        }
        
        #region IDisposable Support
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                if (_connection != null)
                {
                    _connection.Dispose();
                }

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SqlRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
