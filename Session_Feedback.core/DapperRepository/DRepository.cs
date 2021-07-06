using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Session_Feedback.core.DapperRepository
{
    public class DRepository<T> : IDapperRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;

        public IDbConnection DbConnection { get { return _dbConnection; } }
        public DRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public T GetById(string sp, DynamicParameters parms)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            return _dbConnection.QueryFirstOrDefault<T>(sp, param: parms, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetAll(string sp, DynamicParameters parms)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            var result = _dbConnection.Query<T>(sp, param: parms, commandType: CommandType.StoredProcedure);

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

        public int Insert(string sp, DynamicParameters parms)
        {

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            using var tran = _dbConnection.BeginTransaction();
            try
            {
                var result = _dbConnection.QueryFirstOrDefault<int>(sp, parms, commandType: CommandType.StoredProcedure, transaction: tran);

                tran.Commit();
                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        public bool Update(string sp, DynamicParameters parms)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            using var tran = _dbConnection.BeginTransaction();
            try
            {
                var result = _dbConnection.Execute(sp, parms, commandType: CommandType.StoredProcedure, transaction: tran);

                if (result > 0)
                {
                    tran.Commit();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }


        }
    }
}
