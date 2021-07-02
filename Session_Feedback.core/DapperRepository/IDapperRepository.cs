using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Session_Feedback.core.DapperRepository
{
    public interface IDapperRepository<T> where T :class
    {
        Task<T> GetById(string sp, DynamicParameters parms);
        IEnumerable<T> GetAll(string sp);
        Task<T> Insert(string sp, DynamicParameters parms);
        Task<T> Update(string sp, DynamicParameters parms);
        Task<bool> Delete(string sp, int id);
    }
}
