using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetByIdOrName(string sp, DynamicParameters parms);
        IEnumerable<T> GetAll(string sp, DynamicParameters parms);
        int Insert(string sp, DynamicParameters parms);
        bool Update(string sp, DynamicParameters parms);

        //T Find(int id);
        //T FindByName(string name);
        bool Delete(string sp, DynamicParameters parms);
    }
}
