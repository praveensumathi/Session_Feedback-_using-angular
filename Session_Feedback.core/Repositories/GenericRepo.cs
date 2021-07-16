using Dapper;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.Repositories
{
    public class GenericRepo<T> : RepositoryBase, IGenericRepository<T> where T : class
    {
        public GenericRepo(IDbTransaction transaction) : base(transaction)
        {
        }

        public IEnumerable<T> GetAll(string sp, DynamicParameters parms)
        {
            var result = Connection.Query<T>(sp, param: parms, commandType: CommandType.StoredProcedure, transaction: Transaction);

            return result;
        }

        public T GetByIdOrName(string sp, DynamicParameters parms)
        {
            var result = Connection.QueryFirstOrDefault<T>(sp, param: parms, commandType: CommandType.StoredProcedure, transaction: Transaction);
            return result;
        }

        public int Insert(string sp, DynamicParameters parms)
        {
            var result = Connection.QueryFirstOrDefault<int>(sp, parms, commandType: CommandType.StoredProcedure, transaction: Transaction);

            return result;
        }

        public bool Update(string sp, DynamicParameters parms)
        {
            var result = Connection.Execute(sp, parms, commandType: CommandType.StoredProcedure, transaction: Transaction);

            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public bool Delete(string sp,DynamicParameters parms)
        {
            var result = Connection.Execute(sp, commandType: CommandType.StoredProcedure, transaction: Transaction);

            if(result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
