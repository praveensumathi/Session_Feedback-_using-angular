using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Session_Feedback.core.DapperRepository
{
    public interface IDapperRepository<T> where T :class
    {
        T GetById(string sp, DynamicParameters parms);
        IEnumerable<T> GetAll(string sp, DynamicParameters parms);
        int Insert(string sp, DynamicParameters parms);
        bool Update(string sp, DynamicParameters parms);
        Task<bool> Delete(string sp, int id);
    }
}
