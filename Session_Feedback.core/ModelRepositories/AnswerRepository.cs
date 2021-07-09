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
        private readonly string StoreProcedure = "Answer";

        public AnswerRepository(IDbTransaction transaction):base(transaction)
        {

        }

        public IEnumerable<Answer> GetAnswersByQId(int questionId)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@QuestionId", questionId);
            parms.Add("@StatementType", "SelectByQId");

            var answers = Connection.Query<Answer>(StoreProcedure, param: parms, commandType: CommandType.StoredProcedure,transaction : Transaction);

            return answers;
        }
    }
}
