using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.ModelRepositories
{
    public class AnswerRepository : GenericRepo<Answer>
    {
        public AnswerRepository(IDbTransaction transaction):base(transaction)
        {

        }

        public IEnumerable<Answer> GetAnswersByQId(string sp, DynamicParameters parms)
        {
          
            var answers = Connection.Query<Answer>(sp, param: parms, commandType: CommandType.StoredProcedure);

            return answers;
        }
    }
}
