using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.Repositories
{
    public class GenericRepo<T> : RepositoryBase,IGenericRepository<T> where T : class
    {
        private readonly IDbTransaction _transaction;

        public GenericRepo(IDbTransaction transaction) : base(transaction)
        {
            _transaction = transaction;
        }

        public Task<bool> Delete(string sp, int id)
        {
            throw new NotImplementedException();
        }

        //public T Find(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public T FindByName(string name)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<T> GetAll(string sp, DynamicParameters parms)
        {
            var result = Connection.Query<T>(sp, param: parms, commandType: CommandType.StoredProcedure,transaction : Transaction);

            return result;
        }

        public T GetById(string sp, DynamicParameters parms)
        {
            throw new NotImplementedException();
        }

        public int Insert(string sp, DynamicParameters parms)
        {
            throw new NotImplementedException();
        }

        public bool Update(string sp, DynamicParameters parms)
        {
            throw new NotImplementedException();
        }
    }
}
