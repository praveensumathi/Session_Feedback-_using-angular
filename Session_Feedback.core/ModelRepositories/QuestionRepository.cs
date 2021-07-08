using Dapper;
using Session_Feedback.core.Models;
using Session_Feedback.core.Repositories;
using Session_Feedback.core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_Feedback.core.ModelRepositories
{
    public class QuestionRepository : GenericRepo<Question>
    {
        public QuestionRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public IEnumerable<Question> GetQuestionsBySId(string sp, DynamicParameters parms)
        {
            var result = Connection.Query<Question>(sp, param : parms, commandType: CommandType.StoredProcedure,transaction:Transaction);

            return result;
        }
    }
}
