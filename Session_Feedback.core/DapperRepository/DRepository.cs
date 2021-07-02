using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Session_Feedback.core.DapperRepository
{
    public class DRepository<T> : IDapperRepository<T> where T :class
    {
        private readonly IDbConnection _dbConnection;

        public IDbConnection DbConnection { get { return _dbConnection; } }
        public DRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> GetById(string sp, DynamicParameters parms)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();


            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sp, param: parms, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetAll(string sp, DynamicParameters parms)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            var result = _dbConnection.Query<T>(sp, param : parms,commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> Delete(string sp, int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var rowsAffected = await _dbConnection.ExecuteAsync(sp);

            if (rowsAffected != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<T> Insert(string sp, DynamicParameters parms)
        {
            T result;

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            using var tran = _dbConnection.BeginTransaction();
            try
            {
                result = await _dbConnection.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: CommandType.StoredProcedure, transaction: tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }

            return result;
        }

        public async Task<T> Update(string sp, DynamicParameters parms)
        {
            T result;

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            using var tran = _dbConnection.BeginTransaction();
            try
            {
                result = await _dbConnection.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: CommandType.StoredProcedure, transaction: tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }

            return result;

        }
    }
}
