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
        private readonly string StoreProcedure = "usp_Answer";

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

        public bool InsertAnswer(Answer answer)
        {
            var parms = new DynamicParameters();
            parms.Add("@FeedbackAnswer", answer.FeedbackAnswer);
            parms.Add("@AnsweredBy", answer.AnsweredBy);
            parms.Add("@AnsweredOn", DateTime.Now);
            parms.Add("@UserId", answer.UserId);
            parms.Add("@QuestionId", answer.QuestionId);
            parms.Add("@StatementType", "Insert");

            var insertedId = Insert(StoreProcedure, parms);

            if(insertedId > 0)
            {
                return true;
            }
            return false;
        }
    }
}
